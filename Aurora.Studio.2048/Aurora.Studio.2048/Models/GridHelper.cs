﻿using System;
using System.Collections.Generic;
using System.Text;
using Com.Aurora.Shared.Helpers;

namespace Aurora.Studio._2048.Models
{
    class GridHelper
    {
        public static int[] New(out Tile[][] mat)

        {

            mat = new Tile[][] { new Tile[] { new Tile { Data = 0, Row = 0, Col = 0 }, new Tile { Data = 0, Row = 0, Col = 1 }, new Tile { Data = 0, Row = 0, Col = 2 }, new Tile { Data = 0, Row = 0, Col = 3 } }, new Tile[] { new Tile { Data = 0, Row = 1, Col = 0 }, new Tile { Data = 0, Row = 1, Col = 1 }, new Tile { Data = 0, Row = 1, Col = 2 }, new Tile { Data = 0, Row = 1, Col = 3 } }, new Tile[] { new Tile { Data = 0, Row = 2, Col = 0 }, new Tile { Data = 0, Row = 2, Col = 1 }, new Tile { Data = 0, Row = 2, Col = 2 }, new Tile { Data = 0, Row = 2, Col = 3 } }, new Tile[] { new Tile { Data = 0, Row = 3, Col = 0 }, new Tile { Data = 0, Row = 3, Col = 1 }, new Tile { Data = 0, Row = 3, Col = 2 }, new Tile { Data = 0, Row = 3, Col = 3 } } };

            var i = new int[] { 0, 0, 0, 0 };

            var k = Add(ref mat);

            i[0] = k[0];

            i[1] = k[1];

            k = Add(ref mat);

            i[2] = k[0];

            i[3] = k[1];

            return i;

        }



        public static int[] Add(ref Tile[][] matrix)

        {

            int i = 0, j = 0;

            List<int> row = new List<int>();

            List<int> col = new List<int>();

            if (matrix != null && matrix.Length == 4)

            {

                foreach (var m_row in matrix)

                {

                    if (m_row != null && m_row.Length == 4)

                    {

                        j = 0;

                        foreach (var item in m_row)

                        {

                            if (item.Data == 0)

                            {

                                row.Add(i);

                                col.Add(j);

                            }

                            j++;

                        }

                        i++;

                    }

                    else

                    {

                        throw new ArgumentException("Not a valid matrix");

                    }

                }

            }

            else

            {

                throw new ArgumentException("Not a valid matrix");

            }

            if (row.Count == 0)

            {

                return null;

            }

            var num = Tools.Random.Next(row.Count);

            matrix[row[num]][col[num]] = Tools.RandomBool() ? new Tile { Data = 2, Row = row[num], Col = col[num] } : new Tile { Data = 4, Row = row[num], Col = col[num] };

            return new int[] { row[num], col[num] };

        }



        public static void Refresh(ref Tile[][] grid)

        {

            for (int i = 0; i < grid.Length; i++)

            {

                for (int j = 0; j < grid[i].Length; j++)

                {

                    grid[i][j].Row = i;

                    grid[i][j].Col = j;

                }

            }

        }



        public static string Print(Tile[][] matrix)

        {

            var sb = new StringBuilder();

            int i = 0;

            foreach (var row in matrix)

            {

                sb.Append("[" + i + "]: ");

                foreach (var item in row)

                {

                    sb.Append(item.ToString() + ", ");

                }

                sb.Remove(sb.Length - 2, 2);

                sb.Append("\r\n");

                i++;

            }

            return sb.ToString();

        }



        private static void moveLeftBlank(ref Tile[][] matrix)

        {

            foreach (var row in matrix)

            {

                for (int i = 0, j = 1; j < row.Length; i++, j = i + 1)

                {

                    while (j < row.Length && row[j++].Data == 0) ;

                    j--;

                    if (row[j].Data != 0)

                    {

                        if (row[i].Data == 0)

                        {

                            var p = row[i];

                            row[i] = row[j];

                            row[j] = row[i];

                        }

                        else if (j != i + 1)

                        {

                            var p = row[i + 1];

                            row[i + 1] = row[j];

                            row[j] = row[i + 1];

                        }

                    }

                    else

                    {

                        break;

                    }

                }

            }

        }



        public static bool MoveLeft(Tile[][] matrix)

        {

            Tile[][] p;

            int i, j;

            Copy(matrix, out p);

            moveLeftBlank(ref matrix);

            foreach (var row in matrix)

            {

                for (i = 0, j = 1; j < row.Length; i++, j = i + 1)

                {

                    if (row[j].Data == 0)

                    {

                        break;

                    }

                    if (row[i].Data == row[j].Data)

                    {

                        row[i].Data *= 2;

                        row[j].Data = 0;

                    }

                }

            }

            moveLeftBlank(ref matrix);

            return !AreEqual(matrix, p);

        }



        private static void moveRightBlank(ref Tile[][] matrix)

        {

            foreach (var row in matrix)

            {

                for (int i = row.Length - 1, j = row.Length - 2; j > -1; i--, j = i - 1)

                {

                    while (j > -1 && row[j--].Data == 0) ;

                    j++;

                    if (row[j].Data != 0)

                    {

                        if (row[i].Data == 0)

                        {

                            var p = row[i];

                            row[i] = row[j];

                            row[j] = p;

                        }

                        else if (j != i - 1)

                        {

                            var p = row[i - 1];

                            row[i - 1] = row[j];

                            row[j] = row[i - 1];

                        }

                    }

                    else

                    {

                        break;

                    }

                }

            }

        }



        public static bool MoveRight(Tile[][] matrix)

        {

            Tile[][] p;

            int i, j;

            Copy(matrix, out p);

            moveRightBlank(ref matrix);

            foreach (var row in matrix)

            {

                for (i = row.Length - 1, j = row.Length - 2; j > -1; i--, j = i - 1)

                {

                    if (row[j].Data == 0)

                    {

                        break;

                    }

                    if (row[i].Data == row[j].Data)

                    {

                        row[i].Data *= 2;

                        row[j].Data = 0;

                    }

                }

            }

            moveRightBlank(ref matrix);

            return !AreEqual(matrix, p);

        }



        private static void moveUpBlank(ref Tile[][] matrix)

        {

            for (int k = 0; k < matrix[0].Length; k++)

            {

                for (int i = matrix.Length - 1, j = matrix.Length - 2; j > -1; i--, j = i - 1)

                {

                    while (j > -1 && matrix[j--][k].Data == 0) ;

                    j++;

                    if (matrix[j][k].Data != 0)

                    {

                        if (matrix[i][k].Data == 0)

                        {

                            var p = matrix[i][k];

                            matrix[i][k] = matrix[j][k];

                            matrix[j][k] = matrix[i][k];

                        }

                        else if (j != i - 1)

                        {

                            var p = matrix[i - 1][k];

                            matrix[i - 1][k] = matrix[j][k];

                            matrix[j][k] = matrix[i - 1][k];

                        }

                    }

                    else

                    {

                        break;

                    }

                }

            }

        }



        public static bool MoveUp(Tile[][] matrix)

        {

            Tile[][] p;

            int i, j;

            Copy(matrix, out p);

            moveUpBlank(ref matrix);

            for (int k = 0; k < matrix[0].Length; k++)

            {

                for (i = matrix.Length - 1, j = matrix.Length - 2; j > -1; i--, j = i - 1)

                {

                    if (matrix[j][k].Data == 0)

                    {

                        break;

                    }

                    if (matrix[i][k].Data == matrix[j][k].Data)

                    {

                        matrix[i][k].Data *= 2;

                        matrix[j][k].Data = 0;

                    }

                }

            }

            moveUpBlank(ref matrix);

            return !AreEqual(matrix, p);

        }



        private static void moveDownBlank(ref Tile[][] matrix)

        {

            for (int k = 0; k < matrix[0].Length; k++)

            {

                for (int i = 0, j = 1; j < matrix.Length; i++, j = i + 1)

                {

                    while (j < matrix.Length && matrix[j++][k].Data == 0) ;

                    j--;

                    if (matrix[j][k].Data != 0)

                    {

                        if (matrix[i][k].Data == 0)

                        {

                            var p = matrix[i][k];

                            matrix[i][k] = matrix[j][k];

                            matrix[j][k] = matrix[i][k];

                        }

                        else if (j != i + 1)

                        {

                            var p = matrix[i + 1][k];

                            matrix[i + 1][k] = matrix[j][k];

                            matrix[j][k] = matrix[i + 1][k];

                        }

                    }

                    else

                    {

                        break;

                    }

                }

            }

        }



        public static bool MoveDown(Tile[][] matrix)

        {

            Tile[][] p;

            int i, j;

            Copy(matrix, out p);

            moveDownBlank(ref matrix);

            for (int k = 0; k < matrix[0].Length; k++)

            {

                for (i = 0, j = 1; j < matrix.Length; i++, j = i + 1)

                {

                    if (matrix[j][k].Data == 0)

                    {

                        break;

                    }

                    if (matrix[i][k].Data == matrix[j][k].Data)

                    {

                        matrix[i][k].Data *= 2;

                        matrix[j][k].Data = 0;

                    }

                }

            }

            moveDownBlank(ref matrix);

            return !AreEqual(matrix, p);

        }



        public static void Copy(Tile[][] matrix, out Tile[][] p)

        {

            p = new Tile[][] { new Tile[] { new Tile { Data = 0, Row = 0, Col = 0 }, new Tile { Data = 0, Row = 0, Col = 1 }, new Tile { Data = 0, Row = 0, Col = 2 }, new Tile { Data = 0, Row = 0, Col = 3 } }, new Tile[] { new Tile { Data = 0, Row = 1, Col = 0 }, new Tile { Data = 0, Row = 1, Col = 1 }, new Tile { Data = 0, Row = 1, Col = 2 }, new Tile { Data = 0, Row = 1, Col = 3 } }, new Tile[] { new Tile { Data = 0, Row = 2, Col = 0 }, new Tile { Data = 0, Row = 2, Col = 1 }, new Tile { Data = 0, Row = 2, Col = 2 }, new Tile { Data = 0, Row = 2, Col = 3 } }, new Tile[] { new Tile { Data = 0, Row = 3, Col = 0 }, new Tile { Data = 0, Row = 3, Col = 1 }, new Tile { Data = 0, Row = 3, Col = 2 }, new Tile { Data = 0, Row = 3, Col = 3 } } };

            int i = 0;

            int j = 0;

            foreach (var row in matrix)

            {

                j = 0;

                foreach (var item in row)

                {

                    p[i][j] = item;

                    j++;

                }

                i++;

            }

        }



        private static bool AreEqual(Tile[][] matrix, Tile[][] p)

        {

            int i = 0, j = 0;

            foreach (var row in matrix)

            {

                j = 0;

                foreach (var item in row)

                {

                    if (p[i][j].Data != item.Data)

                    {

                        return false;

                    }

                    j++;

                }

                i++;

            }

            return true;

        }

    }
}

public class Tile : IEquatable<Tile>

{

    public uint Data;

    public int Row;

    public int Col;



    public bool Equals(Tile other)

    {

        return (Data == other.Data && Row == other.Row && Col == other.Col);

    }



    public override string ToString()

    {

        return "Data = " + Data + ", Row = " + Row + ", Col = " + Col;

    }

}