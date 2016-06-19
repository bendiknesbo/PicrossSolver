using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Domain.Helpers;

namespace Domain.Picross {
    public class PicrossBruteForcer : SolverBase {

        private List<Color[,]> _grids = new List<Color[,]>();
        public PicrossBruteForcer(int rowCount, int colCount, List<Classifier> rows, List<Classifier> columns)
            : base(rowCount, colCount, rows, columns) {
        }

        public override void Solve() {
            GenerateAllGrids();
            var valid = _grids.FirstOrDefault(g => g.IsSolved(Rows, Columns));
            WorkingGrid = valid ?? WorkingGrid;
        }

        private void GenerateAllGrids() {
            //todo: Actually generate all possible grids...

            /*
             * Pseudokode:
             * For alle rader: Lag alle permutasjoner av celler
             * For alle permutasjoner av rader: Lag alle permutasjoner av grid totalt sett.
             */
            var dict = new Dictionary<int, List<Color[]>>();
            foreach (var row in Rows) {
                var temp = new Color[ColCount];
                int col = 0;
                foreach (var color in Rows[row.Index].Colors) {
                    for (int i = 0; i < color.Count; i++) {
                        temp[col++] = color.MyColor;
                    }
                }
                dict[row.Index] = Permute(temp);
            }
            //at this point, each row in dict contains every permutation of the cells in that row
            GenerateAllGrids(dict);
        }

        private void GenerateAllGrids(Dictionary<int, List<Color[]>> dict) {
            Color[,] temp = new Color[RowCount, ColCount];
            var permDict = dict.ToDictionary(k => k.Key, v => new Permutation { TotalPermutations = v.Value.Count });
            for (int rowIdx = 0; rowIdx < RowCount && rowIdx > -1; rowIdx++) {
                var permValue = permDict[rowIdx];
                if (permValue.CurrentPermutation == permValue.TotalPermutations) {
                    foreach (var kvp in permDict.Where(kvp => kvp.Key >= rowIdx)) {
                        kvp.Value.CurrentPermutation = 0;
                    }
                    rowIdx -= 2;
                    continue;
                }
                ApplyRow(temp, rowIdx, dict[rowIdx][permValue.CurrentPermutation++]);
                if (rowIdx == RowCount - 1) {
                    _grids.Add(temp.DeepCopy());
                    rowIdx--;
                }
            }
        }

        private class Permutation {
            public int TotalPermutations;
            public int CurrentPermutation;
        }

        private void ApplyRow(Color[,] matrix, int rowIdx, Color[] rowArr) {
            for (int i = 0; i < rowArr.Length; i++) {
                matrix[rowIdx, i] = rowArr[i];
            }
        }

        private static List<Color[]> Permute(Color[] arr) {
            var list = new List<Color[]>();
            Permute(list, arr, 0, arr.Length - 1);
            return list;
        }
        private static void Permute(List<Color[]> list, Color[] arr, int i, int n) {
            if (i == n) {
                list.Add(arr.DeepCopy());
            } else {
                for (int j = i; j <= n; j++) {
                    Swap(ref arr[i], ref arr[j]);
                    Permute(list, arr, i + 1, n);
                    Swap(ref arr[i], ref arr[j]); //backtrack
                }
            }
        }

        private static void Swap(ref Color a, ref Color b) {
            var tmp = a;
            a = b;
            b = tmp;
        }
        /*
        public void GetPer(char[] list) {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private static void GetPer(char[] list, int k, int m) {
            if (k == m) {
                Console.Write(list);
            } else
                for (int i = k; i <= m; i++) {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }
         * */

    }
}
