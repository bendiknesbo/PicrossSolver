using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Domain.Enums;
using Domain.Helpers;

namespace Domain.Picross {
    public class PicrossSolver {
        public List<Classifier> Rows { get; private set; }
        public List<Classifier> Columns { get; private set; }

        public Color[,] WorkingGrid;
        public Color[,] AnswerGrid = null;
        private readonly int _rowCount;
        private readonly int _colCount;
        private readonly int _cellCount;
        private int _paintedCount;
        private bool _isDirty;
        private Func<int, Color[]> _getArray;
        private int _selectionCount;
        private List<Classifier> _items;
        private Classifier _currentItem;
        private Selection _selection;
        private Func<int, int, Color> _getCell;
        private List<Classifier> _oppositeItems;
        private ColorClassifier _currentColor;
        private string _readableString;
        private int _iterationCounter;
        public const int OutOfBoundsConst = -1;

        public List<Action> SolvePartActions;

        public PicrossSolver(int rowCount, int colCount, List<Classifier> rows, List<Classifier> columns) {
            _colCount = colCount;
            _rowCount = rowCount;
            _cellCount = _rowCount * _colCount;
            Columns = columns;
            Rows = rows;
            WorkingGrid = new Color[rowCount, colCount];
            _iterationCounter = 0;
            //All Actions in this list should start with "Solve_Part_"
            SolvePartActions = new List<Action>{
                Solve_Part_WasAlreadySolved,
                Solve_Part_WholeRowOrColumnIsSameColor,
                Solve_Part_OnlyThisColorLeftInItem,
                Solve_Part_FillConnectedFromStart,
                Solve_Part_FillConnectedFromEnd,
                Solve_Part_PartiallyFillConnectedFromStart,
                Solve_Part_PartiallyFillConnectedFromEnd,
                Solve_Part_PartiallyFillConnectedWithMoreThanHalf,
                Solve_Part_FillBetweenConnected,
                Solve_Part_OnlyTwoColorsInItem_OtherColorIsConnected,
                Solve_Part_Temp1,
                Solve_Part_Temp2,
                Solve_Part_Temp3,
                Solve_Part_OnlyOnePossibleBlockSetWhereConnectedFits,
            };
        }

        public void Solve() {
            _readableString = string.Empty;
            do {
                SolveActually();
                SolveActually();
                SolveActually();
                if (_cellCount == _paintedCount) break;
            } while (_isDirty);
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
                IterateCells();
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

        /// <summary>
        /// Might be slow and not actually solve anything... possibly remove in futue...
        /// </summary>
        private void IterateCells() {
            for (int rowIdx = 0; rowIdx < _rowCount; rowIdx++) {
                for (int colIdx = 0; colIdx < _colCount; colIdx++) {
                    var row = Rows.First(r => r.Index == rowIdx);
                    var col = Columns.First(c => c.Index == colIdx);
                    var rowColors = row.Colors.Where(c => !c.IsDone);
                    var colColors = col.Colors.Where(c => !c.IsDone);
                    var possibleColorsForCell = rowColors.Intersect(colColors).ToList();
                    if (possibleColorsForCell.Count == 1) {
                        FillCellAndSetDirty(rowIdx, colIdx, possibleColorsForCell.Single().MyColor);
                    }
                }
            }
        }

        //Before refactor: Number of failing tests: 38
        //Before refactor: Number of failing tests: 300
        private void Iterate(Selection selection) {
            SetupSelectionAndFields(selection);

            foreach (var item in _items) {
                _currentItem = item;

                foreach (var colorClassifierTemp in _currentItem.Colors) {
                    _currentColor = colorClassifierTemp;
                    foreach (var action in SolvePartActions) {
                        if (SolvePart(action))
                            break;
                    }
                }
            }
        }

        private bool SolvePart(Action part) {
            part();
            return _currentColor.IsDone;
        }

        private void Solve_Part_WasAlreadySolved() {
            if (_currentColor.IsDone)
                return;
            if (_currentColor.Count == 0 || _currentColor.Count == FindNumberOfElementsInSelection()) {
                _currentColor.IsDone = true;
                _isDirty = true;
            }
        }

        private void Solve_Part_WholeRowOrColumnIsSameColor() {
            if (_currentColor.Count == _selectionCount) {
                FillSelection(0, _selectionCount);
                _currentColor.IsDone = true;
            }
        }

        private void Solve_Part_OnlyThisColorLeftInItem() {
            var others = _currentItem.Colors.Where(cc => cc.MyColor != _currentColor.MyColor);
            if (others.All(cc => cc.IsDone)) {
                Color[] workingArray = _getArray(_currentItem.Index);
                var workingDict = Enumerable.Range(0, workingArray.Length).ToDictionary(x => x, x => workingArray[x]);
                FillCells(workingDict.Where(kvp => kvp.Value.Equals(Color.Empty)).Select(kvp => kvp.Key).ToList());
                _currentColor.IsDone = true;
            }
        }

        private void Solve_Part_FillConnectedFromStart() {
            if (!_currentColor.IsConnected)
                return;
            Color[] workingArray = _getArray(_currentItem.Index);
            var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
            if (firstIndex == 0) {
                //Eksempel: 2,0,0,0,0 + Count=3 -> 2,2,2,0,0
                FillSelection(startIndex: firstIndex, endIndex: _currentColor.Count);
                _currentColor.IsDone = true;
            }
        }

        private void Solve_Part_FillConnectedFromEnd() {
            if (!_currentColor.IsConnected)
                return;
            Color[] workingArray = _getArray(_currentItem.Index);
            var lastIndex = LastIndexOf(workingArray, _currentColor.MyColor);
            if (lastIndex == _selectionCount) {
                //Eksempel: 0,0,0,0,2 + Count=3 -> 0,0,2,2,2
                FillSelection(startIndex: lastIndex - _currentColor.Count, endIndex: lastIndex);
                _currentColor.IsDone = true;
            }
        }

        private void Solve_Part_FillBetweenConnected() {
            if (!_currentColor.IsConnected)
                return;
            Color[] workingArray = _getArray(_currentItem.Index);
            var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
            var lastIndex = LastIndexOf(workingArray, _currentColor.MyColor);
            if (firstIndex > OutOfBoundsConst && lastIndex > firstIndex) {
                //Eksempel: 0,2,0,2,0 + Count=4 -> 0,2,2,2,0
                FillSelection(startIndex: firstIndex, endIndex: lastIndex);
            }
        }

        private void Solve_Part_PartiallyFillConnectedFromStart() {
            if (!_currentColor.IsConnected)
                return;
            Color[] workingArray = _getArray(_currentItem.Index);
            var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
            if (firstIndex > OutOfBoundsConst) {
                if (0 + _currentColor.Count > firstIndex) {
                    int extraToTheLeft = 0;
                    var workingDict = Enumerable.Range(0, workingArray.Length).ToDictionary(x => x, x => workingArray[x]);
                    if (workingDict.Any(kvp => kvp.Key < firstIndex && kvp.Value.Equals(Color.Empty))) {
                        extraToTheLeft = workingDict.First(kvp => kvp.Key < firstIndex && kvp.Value.Equals(Color.Empty)).Key;
                    }
                    //Eksempel: 0,2,0,0,0 + Count=3 -> 0,2,2,0,0
                    FillSelection(startIndex: firstIndex, endIndex: extraToTheLeft + _currentColor.Count);
                }
            }
        }
        private void Solve_Part_PartiallyFillConnectedFromEnd() {
            if (!_currentColor.IsConnected)
                return;
            Color[] workingArray = _getArray(_currentItem.Index);
            var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
            var lastIndex = LastIndexOf(workingArray, _currentColor.MyColor);
            if (firstIndex > OutOfBoundsConst) {
                if (_selectionCount - _currentColor.Count < lastIndex) {
                    //Eksempel: 0,0,0,2,0 + Count=3 -> 0,0,2,2,0
                    FillSelection(startIndex: _selectionCount - _currentColor.Count, endIndex: lastIndex);
                }

            }
        }
        private void Solve_Part_PartiallyFillConnectedWithMoreThanHalf() {
            if (!_currentColor.IsConnected)
                return;
            //TODO: Allow padding from both sides.
            int half = _selectionCount / 2;
            if (_currentColor.Count > half) {
                FillSelection(startIndex: _selectionCount - _currentColor.Count, endIndex: _currentColor.Count);
            }
        }

        private void Solve_Part_OnlyTwoColorsInItem_OtherColorIsConnected() {
            if (_currentColor.IsConnected)
                return;
            //todo: kan eg få til denne men med ferdig utfylte plasser?
            var count = _currentItem.Colors.Count(cc => cc.Count > 0);
            if (count == 2) {
                var other = _currentItem.Colors.First(cc => !cc.MyColor.Equals(_currentColor.MyColor));
                if (other.IsConnected) {
                    FillCells(new List<int> { 0, _selectionCount - 1 });
                }
            }
        }
        private void Solve_Part_Temp1() {
            var possibleSpots = _oppositeItems.Where(o => o.Colors.Any(cc => cc.MyColor == _currentColor.MyColor && cc.Count > 0)).ToList();
            if (possibleSpots.Count == _currentColor.Count) {
                FillCells(possibleSpots.Select(c => c.Index).ToList());
                _currentColor.IsDone = true;
                return;
            } else {
                //fleire mulige enn antall som skal til.
                if (_currentColor.IsConnected) {
                    Color[] workingArray = _getArray(_currentItem.Index);
                    var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
                    var lastIndex = LastIndexOf(workingArray, _currentColor.MyColor);
                    if (firstIndex <= OutOfBoundsConst || lastIndex <= OutOfBoundsConst) {
                        //do nothing
                    } else if (firstIndex - 1 <= OutOfBoundsConst) {
                        //
                    } else if (lastIndex + 1 > _selectionCount - 1) {
                        //out of bounds
                    } else {
                        var cellBefore = _getCell(_currentItem.Index, firstIndex - 1);
                        var cellAfter = _getCell(_currentItem.Index, lastIndex + 1); //må cache dette resultatet, da den neste FillSelection kan endre den!
                        if (cellBefore.Equals(Color.Empty)) {
                            //hmm...
                        } else if (cellBefore != _currentColor.MyColor) {
                            FillSelection(startIndex: firstIndex, endIndex: firstIndex + _currentColor.Count);
                        } else {
                            throw new Exception("Wat, this should never happen!");
                        }
                        if (cellAfter.Equals(Color.Empty)) {
                            //hmm...
                        } else if (cellAfter != _currentColor.MyColor) {
                            FillSelection(startIndex: lastIndex - _currentColor.Count + 1, endIndex: lastIndex);
                        } else {
                            throw new Exception("Wat??? breakpoint her...");
                        }
                    }
                } else if (!_currentColor.IsConnected && _currentColor.Count == 2) {
                    //todo: Meir generell??
                    Color[] workingArray = _getArray(_currentItem.Index);
                    var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
                    if (firstIndex <= OutOfBoundsConst) {
                        var workingDict = Enumerable.Range(0, workingArray.Length).ToDictionary(x => x, x => workingArray[x]);
                        var temp = workingDict.Where(kvp => kvp.Value.Equals(Color.Empty)).ToDictionary();
                        var first = temp.First().Key;
                        if (temp.Count() == 3 && temp.ContainsKey(first + 1) && temp.ContainsKey(first + 2)) {
                            FillCells(new List<int> { first, first + 2 });
                        }
                    } else {
                        var newPossibleSpots = possibleSpots.Where(kvp => !(kvp.Index >= firstIndex - 1 && kvp.Index <= firstIndex + 1)).ToList();
                        newPossibleSpots = newPossibleSpots.Where(kvp => kvp.Colors.Any(cc => cc.MyColor == _currentColor.MyColor && cc.Count > 0 && cc.IsDone != true)).ToList();
                        if (newPossibleSpots.Count == _currentColor.Count - 1) {
                            FillCells(newPossibleSpots.Select(c => c.Index).ToList());
                            _currentColor.IsDone = true;
                            return;
                        }
                    }
                } else {
                    //todo...
                }
            }
        }
        private void Solve_Part_Temp2() {
            var possible2Spots = _oppositeItems.Where(o => o.Colors.Any(cc => cc.MyColor == _currentColor.MyColor && cc.Count > 0 && !cc.IsDone)).ToList();
            var possible2Keys = possible2Spots.Select(kvp => kvp.Index).ToList();
            //note: possibleSpots2 includes the ones that are colored in..
            Color[] workingArray2 = _getArray(_currentItem.Index);
            possible2Keys.RemoveAll(i => !workingArray2[i].Equals(Color.Empty));
            var countStillNeeded = _currentColor.Count - workingArray2.Count(i => i.Equals(_currentColor.MyColor));
            if (possible2Keys.Count == countStillNeeded) {
                FillCells(possible2Keys);
                _currentColor.IsDone = true;
                return;
            }
        }

        private void Solve_Part_Temp3() {
            if (_currentColor.IsConnected) {
                Color[] workingArray = _getArray(_currentItem.Index);
                var firstIndex = IndexOf(workingArray, _currentColor.MyColor);
                var lastIndex = LastIndexOf(workingArray, _currentColor.MyColor);
                if (firstIndex <= OutOfBoundsConst) {
                    //do nothing
                } else {
                    var workingDict2 = Enumerable.Range(0, workingArray.Length).ToDictionary(x => x, x => workingArray[x]);
                    workingDict2 = FilterAwayNonTouchingSlots(workingDict2, firstIndex, _currentColor.MyColor);
                    if (firstIndex == workingDict2.First().Key) {
                        FillSelection(startIndex: firstIndex, endIndex: firstIndex + _currentColor.Count);
                        _currentColor.IsDone = true;
                        return;
                    } else if (lastIndex == workingDict2.Last().Key) {
                        FillSelection(startIndex: lastIndex - _currentColor.Count + 1, endIndex: workingDict2.Last().Key);
                        _currentColor.IsDone = true;
                        return;
                    } else {
                        if (workingDict2.First().Key + _currentColor.Count > firstIndex) {
                            FillSelection(startIndex: firstIndex, endIndex: workingDict2.First().Key + _currentColor.Count);
                        }
                        if (workingDict2.Last().Key - _currentColor.Count + 1 < lastIndex) {
                            FillSelection(startIndex: workingDict2.Last().Key - _currentColor.Count + 1, endIndex: lastIndex);
                        }
                        FillSelection(startIndex: firstIndex, endIndex: lastIndex);
                    }
                }
            }
        }

        private void Solve_Part_OnlyOnePossibleBlockSetWhereConnectedFits() {
            if (!_currentColor.IsConnected)
                return;
            Color[] workingArray = _getArray(_currentItem.Index);
            var workingDict = Enumerable.Range(0, workingArray.Length).ToDictionary(x => x, x => workingArray[x]);
            var intRangeList = FindBlockSetsWhereConnectedFits(workingDict);
            if (intRangeList.Count == 1) {
                var intRange = intRangeList.Single();
                if (intRange.EndIndex - intRange.StartIndex + 1 == _currentColor.Count)
                    FillSelection(startIndex: intRange.StartIndex, endIndex: intRange.EndIndex);
            }
        }

        private List<IntRange> FindBlockSetsWhereConnectedFits(Dictionary<int, Color> dict) {
            return FindPossibleBlockSets(dict).Where(ir => ir.EndIndex - ir.StartIndex + 1 >= _currentColor.Count).ToList();
        }

        private List<IntRange> FindPossibleBlockSets(Dictionary<int, Color> dict) {
            var list = new List<IntRange>();
            var tempRange = new IntRange();
            for (int i = 0; i < _selectionCount; i++) {
                var cell = dict[i];
                if (tempRange.StartIndex == OutOfBoundsConst) {
                    if (cell.Equals(_currentColor.MyColor) || cell.Equals(Color.Empty)) {
                        tempRange.StartIndex = i;
                        continue;
                    }
                } else {
                    if (!cell.Equals(_currentColor.MyColor) && !cell.Equals(Color.Empty)) {
                        tempRange.EndIndex = i - 1;
                        list.Add(tempRange);
                        tempRange = new IntRange();
                        continue;
                    }
                }
            }
            if (tempRange.StartIndex != OutOfBoundsConst && tempRange.EndIndex == OutOfBoundsConst) {
                tempRange.EndIndex = _selectionCount - 1;
                list.Add(tempRange);
            }
            return list;
        }

        private Dictionary<int, Color> FilterAwayNonTouchingSlots(Dictionary<int, Color> openSlots, int anyIndex, Color color) {
            var tempDict = new Dictionary<int, Color>();
            int tempIndex = anyIndex;
            while (tempIndex >= 0) {
                var cell = openSlots[tempIndex];
                if (!cell.Equals(color) && !cell.Equals(Color.Empty)) {
                    break;
                }
                tempDict[tempIndex] = cell;
                tempIndex--;
            }
            tempIndex = anyIndex;
            while (tempIndex < _selectionCount) {
                var cell = openSlots[tempIndex];
                if (!cell.Equals(color) && !cell.Equals(Color.Empty)) {
                    break;
                }
                tempDict[tempIndex] = cell;
                tempIndex++;
            }
            return tempDict.OrderBy(kvp => kvp.Key).ToDictionary();
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

        private int FindNumberOfElementsInSelection() {
            int count = 0;
            var actualRow = _getArray(_currentItem.Index);
            for (int i = 0; i < actualRow.Length; i++) {
                if (_getCell(_currentItem.Index, i) == _currentColor.MyColor)
                    count++;
            }
            return count;
        }

        private void FillCells(List<int> oppositeItemNumbers) {
            if (_selection == Selection.Row)
                ActualFillCellsInRow(colNumbers: oppositeItemNumbers);
            else
                ActualFillCellsInColumn(rowNumbers: oppositeItemNumbers);
        }

        private void ActualFillCellsInRow(List<int> colNumbers) {
            var row = _currentItem.Index;
            foreach (var col in colNumbers) {
                FillCellAndSetDirty(row: row, column: col);
            }
        }

        private void ActualFillCellsInColumn(List<int> rowNumbers) {
            var column = _currentItem.Index;
            foreach (var row in rowNumbers) {
                FillCellAndSetDirty(row: row, column: column);
            }
        }

        private void FillSelection(int startIndex, int endIndex) {
            if (_selection == Selection.Row)
                ActualFillRow(startIndex, endIndex);
            else
                ActualFillColumn(startIndex, endIndex);
        }

        private void ActualFillRow(int startIndex, int endIndex) {
            var row = _currentItem.Index;
            for (int i = startIndex; i < endIndex; i++) {
                FillCellAndSetDirty(row, i);
            }
        }

        private void ActualFillColumn(int startIndex, int endIndex) {
            var column = _currentItem.Index;
            for (int i = startIndex; i < endIndex; i++) {
                FillCellAndSetDirty(i, column);
            }
        }

        private void FillCellAndSetDirty(int row, int column, Color? colorToFill = null) {
            var color = colorToFill ?? _currentColor.MyColor;
            if (WorkingGrid[row, column].Equals(Color.Empty)) {
                WorkingGrid[row, column] = color;
                _paintedCount++;
                _isDirty = true;
            }
#if DEBUG
            _readableString = WorkingGrid.ToReadableString();
            if (AnswerGrid != null && AnswerGrid[row, column] != color)
                throw new Exception(string.Format("Oops, wrong color! Expected: <{0}>. Actual: <{1}>", AnswerGrid[row, column], color));
#endif
        }
    }
}