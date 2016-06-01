using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Domain {
    public class PicrossSolver {
        public Dictionary<int, Classifier> Rows { get; private set; }
        public Dictionary<int, Classifier> Columns { get; private set; }

        public Color[,] WorkingGrid;
        private readonly int _rowCount;
        private readonly int _colCount;
        private readonly int _cellCount;
        private int _paintedCount;
        private bool _isDirty;
        private Func<int, Color[]> _getArray;
        private int _selectionCount;
        private Dictionary<int, Classifier> _items;
        private Selection _selection;
        private Func<int, int, Color> _getCell;
        private int _iterationCounter;
        private string _readableString;
        private Dictionary<int, Classifier> _oppositeItems;
        private const int OutOfBoundsConst = -1;

        public PicrossSolver(int rowCount, int colCount, Dictionary<int, Classifier> rows, Dictionary<int, Classifier> columns) {
            _colCount = colCount;
            _rowCount = rowCount;
            _cellCount = _rowCount * _colCount;
            Columns = columns;
            Rows = rows;
            WorkingGrid = new Color[rowCount, colCount];
        }

        public void Solve() {
            _readableString = string.Empty;
            do {
                _iterationCounter = 0;
                SolveActually();
                if (_cellCount == _paintedCount) break;
            } while (_isDirty || _iterationCounter <= 3);
        }

        private void SolveActually() {

#if DEBUG
            _readableString = WorkingGrid.ToReadableString();
#endif
            do {
                _iterationCounter++;
                _isDirty = false;
                Iterate(Selection.Row);
                Iterate(Selection.Column);
                if (_cellCount == _paintedCount) break;
            } while (_isDirty);
        }

        private void SetupSelectionAndFields(Selection selection) {
            _selection = selection;
            if (_selection == Selection.Row) {
                _items = Rows;
                _oppositeItems = Columns;
                _selectionCount = _colCount;
                _getArray = (rowNumber => WorkingGrid.GetRow(rowNumber));
                _getCell = ((x, y) => WorkingGrid[x, y]);
            } else {
                _items = Columns;
                _oppositeItems = Rows;
                _selectionCount = _rowCount;
                _getArray = (colNumber => WorkingGrid.GetColumn(colNumber));
                _getCell = ((x, y) => WorkingGrid[y, x]);
            }
        }

        private void Iterate(Selection selection) {
            SetupSelectionAndFields(selection);

            foreach (var item in _items) {
                var itemNumber = item.Key;
                var itemClassifier = item.Value;

                foreach (var colorClassKvp in itemClassifier.Colors) {
                    var myColor = colorClassKvp.Key;
                    var colorClassifier = colorClassKvp.Value;

                    if (colorClassifier.Count == 0 || colorClassifier.IsDone || colorClassifier.Count == FindNumberOfElementsInSelection(itemNumber, myColor))
                        colorClassifier.IsDone = true;
                    if (colorClassifier.IsDone) continue;
                    if (colorClassifier.Count == _selectionCount) {
                        FillSelection(itemNumber, myColor);
                        colorClassifier.IsDone = true;
                        continue;
                    }
                    var others = itemClassifier.Colors.Values.Where(cc => cc.MyColor != colorClassifier.MyColor);
                    if (others.All(cc => cc.IsDone)) {
                        FillSelection(itemNumber, myColor);
                        colorClassifier.IsDone = true;
                        continue;
                    }
                    if (colorClassifier.IsConnected) {
                        Color[] workingArray = _getArray(itemNumber);
                        var firstIndex = IndexOf(workingArray, myColor);
                        var lastIndex = LastIndexOf(workingArray, myColor);
                        if (firstIndex <= OutOfBoundsConst || lastIndex <= OutOfBoundsConst) {
                            //do nothing
                        } else if (firstIndex == 0) {
                            //Eksempel: 2,0,0,0,0 + Count=3 -> 2,2,2,0,0
                            FillSelection(itemNumber, myColor, startIndex: firstIndex, endIndex: colorClassifier.Count);
                            colorClassifier.IsDone = true;
                            continue;
                        } else if (lastIndex == _selectionCount) {
                            //Eksempel: 0,0,0,0,2 + Count=3 -> 0,0,2,2,2
                            FillSelection(itemNumber, myColor, startIndex: lastIndex - colorClassifier.Count, endIndex: lastIndex);
                            colorClassifier.IsDone = true;
                            continue;
                        } else {
                            if (0 + colorClassifier.Count > firstIndex) {
                                //Eksempel: 0,2,0,0,0 + Count=3 -> 0,2,2,0,0
                                FillSelection(itemNumber, myColor, startIndex: firstIndex, endIndex: 0 + colorClassifier.Count);
                            }
                            if (_selectionCount - colorClassifier.Count < lastIndex) {
                                //Eksempel: 0,0,0,2,0 + Count=3 -> 0,0,2,2,0
                                FillSelection(itemNumber, myColor, startIndex: _selectionCount - colorClassifier.Count, endIndex: lastIndex);
                            }
                            //Eksempel: 0,2,0,2,0 + Count=4 -> 0,2,2,2,0
                            FillSelection(itemNumber, myColor, startIndex: firstIndex, endIndex: lastIndex);
                            //continue?
                        }
                    }
                    if (!colorClassifier.IsConnected) {
                        //todo: kan eg få til denne men med ferdig utfylte plasser?
                        var count = itemClassifier.Colors.Count(cc => cc.Value.Count > 0);
                        if (count == 2) {
                            var other = itemClassifier.Colors.First(cc => !cc.Key.Equals(myColor));
                            if (other.Value.IsConnected) {
                                FillCells(itemNumber, myColor, new List<int> { 0, _selectionCount - 1 });
                                //continue;
                            }
                        }
                    }

                    var possibleSpots = _oppositeItems.Where(o => o.Value.Colors.Any(cc => cc.Key == myColor && cc.Value.Count > 0)).ToDictionary(k => k.Key, v => v.Value);
                    if (possibleSpots.Count == colorClassifier.Count) {
                        FillCells(itemNumber, myColor, possibleSpots.Keys.ToList());
                        colorClassifier.IsDone = true;
                        continue;
                    } else {
                        //fleire mulige enn antall som skal til.
                        if (colorClassifier.IsConnected) {
                            Color[] workingArray = _getArray(itemNumber);
                            var firstIndex = IndexOf(workingArray, myColor);
                            var lastIndex = LastIndexOf(workingArray, myColor);
                            if (firstIndex <= OutOfBoundsConst || lastIndex <= OutOfBoundsConst) {
                                //do nothing
                            } else if (firstIndex - 1 <= OutOfBoundsConst) {
                                //
                            } else if (lastIndex + 1 > _selectionCount - 1) {
                                //out of bounds
                            } else {
                                var cellBefore = _getCell(itemNumber, firstIndex - 1);
                                var cellAfter = _getCell(itemNumber, lastIndex + 1); //må cache dette resultatet, da den neste FillSelection kan endre den!
                                if (cellBefore.Equals(Color.Empty)) {
                                    //hmm...
                                } else if (cellBefore == myColor) {
                                    Console.WriteLine("Wat??? breakpoint her...");
                                } else {
                                    FillSelection(itemNumber, myColor, firstIndex, firstIndex + colorClassifier.Count);
                                }

                                if (cellAfter.Equals(Color.Empty)) {
                                    //hmm...
                                } else if (cellAfter == myColor) {
                                    Console.WriteLine("Wat??? breakpoint her...");
                                } else {
                                    FillSelection(itemNumber, myColor, lastIndex - colorClassifier.Count + 1, lastIndex);
                                }
                            }
                        } else if (!colorClassifier.IsConnected && colorClassifier.Count == 2) {
                            //todo: Meir generell??
                            Color[] workingArray = _getArray(itemNumber);
                            var firstIndex = IndexOf(workingArray, myColor);
                            if (firstIndex <= OutOfBoundsConst) {
                                //do nothing
                            } else {
                                var newPossibleSpots = possibleSpots.Where(kvp => !(kvp.Key >= firstIndex - 1 && kvp.Key <= firstIndex + 1)).ToDictionary(k => k.Key, v => v.Value);
                                newPossibleSpots = newPossibleSpots.Where(kvp => kvp.Value.Colors.Any(cc => cc.Key == myColor && cc.Value.Count > 0 && cc.Value.IsDone != true)).ToDictionary(k => k.Key, v => v.Value);
                                if (newPossibleSpots.Count == colorClassifier.Count - 1) {
                                    FillCells(itemNumber, myColor, newPossibleSpots.Keys.ToList());
                                    colorClassifier.IsDone = true;
                                    continue;
                                }
                            }
                        } else {
                            //todo...
                        }
                    }

                    var possible2Spots = _oppositeItems.Where(o => o.Value.Colors.Any(cc => cc.Key == myColor && cc.Value.Count > 0 && !cc.Value.IsDone)).ToDictionary(k => k.Key, v => v.Value);
                    var possible2Keys = possible2Spots.Select(kvp => kvp.Key).ToList();
                    //note: possibleSpots2 includes the ones that are colored in..
                    Color[] workingArray2 = _getArray(itemNumber);
                    possible2Keys.RemoveAll(i => !workingArray2[i].Equals(Color.Empty));
                    var countStillNeeded = colorClassifier.Count - workingArray2.Count(i => i.Equals(myColor));
                    if (possible2Keys.Count == countStillNeeded) {
                        FillCells(itemNumber, myColor, possible2Keys);
                        colorClassifier.IsDone = true;
                        continue;
                    }

                }
            }
        }

        private int IndexOf(Color[] arr, Color color) {
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] == color)
                    return i;
            }
            return OutOfBoundsConst;
        }

        private int LastIndexOf(Color[] arr, Color color) {
            for (int i = arr.Length - 1; i >= 0; i--) {
                if (arr[i] == color)
                    return i;
            }
            return OutOfBoundsConst;
        }

        private int FindNumberOfElementsInSelection(int index, Color color) {
            int count = 0;
            var actualRow = _getArray(index);
            for (int i = 0; i < actualRow.Length; i++) {
                if (_getCell(index, i) == color)
                    count++;
            }
            return count;
        }


        private void FillCells(int itemNumber, Color color, List<int> oppositeItemNumbers) {
            if (_selection == Selection.Row)
                ActualFillCellsInRow(itemNumber, color, colNumbers: oppositeItemNumbers);
            else
                ActualFillCellsInColumn(itemNumber, color, rowNumbers: oppositeItemNumbers);
        }

        private void ActualFillCellsInRow(int row, Color color, List<int> colNumbers) {
            foreach (var col in colNumbers) {
                FillCellAndSetDirty(row: row, column: col, color: color);
            }
        }

        private void ActualFillCellsInColumn(int column, Color color, List<int> rowNumbers) {
            foreach (var row in rowNumbers) {
                FillCellAndSetDirty(row: row, column: column, color: color);
            }
        }

        private void FillSelection(int itemNumber, Color myColor, int? startIndex = null, int? endIndex = null) {
            if (!startIndex.HasValue) startIndex = 0;
            if (!endIndex.HasValue) {
                endIndex = _selection == Selection.Row ? _colCount : _rowCount;
            }
            if (_selection == Selection.Row)
                ActualFillRow(itemNumber, myColor, startIndex.Value, endIndex.Value);
            else
                ActualFillColumn(itemNumber, myColor, startIndex.Value, endIndex.Value);
        }

        private void ActualFillRow(int row, Color color, int startIndex, int endIndex) {
            for (int i = startIndex; i < endIndex; i++) {
                FillCellAndSetDirty(row, i, color);
            }
        }

        private void ActualFillColumn(int column, Color color, int startIndex, int endIndex) {
            for (int i = startIndex; i < endIndex; i++) {
                FillCellAndSetDirty(i, column, color);
            }
        }

        private void FillCellAndSetDirty(int row, int column, Color color) {
            if (WorkingGrid[row, column].Equals(Color.Empty)) {
                WorkingGrid[row, column] = color;
                _paintedCount++;
                _isDirty = true;
#if DEBUG
                _readableString = WorkingGrid.ToReadableString();
#endif
            }
        }
    }
}