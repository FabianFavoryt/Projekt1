﻿using System;
using System.Collections.Generic;


namespace Mittprojekt
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int round = 0;
            while (true)
            {
                int choice = 0;
                writeBoard(board);

                if (round % 2 == 0)
                {
                    choice = playerChoice(board);
                    board = move(board, choice, 'X');
                }
                else
                {
                    choice = playerChoice(board);
                    board = move(board, choice, 'O');
                }

                string state = checkWin(board);

                if (state == "win")
                {
                    writeBoard(board);
                    Console.WriteLine("X vann!");
                    break;
                }

                if (state == "lose")
                {
                    writeBoard(board);
                    Console.WriteLine("O vann!");
                    break;
                }

                if (state == "tie")
                {
                    writeBoard(board);
                    Console.WriteLine("Det blev lika");
                    break;
                }

                round += 1;

                Console.Clear();
            }
        }

        static void writeBoard(char[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write($"| {board[i]} ");
                if ((i + 1) % 3 == 0)
                {
                    Console.Write("|\n");
                }
            }
        }

        static int playerChoice(char[] board)
        {
            Console.Write("Skriv in val: ");
            int choice = 0;
            List<int> freeList = getEmptySquare(board);
            bool valid = false;
            try
            {
                choice = int.Parse(Console.ReadLine()) - 1;
                for (int i = 0; i < freeList.Count; i++)
                {
                    if (choice == freeList[i])
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Error");
                choice = playerChoice(board);
            }

            return choice;
        }

        static char[] move(char[] board, int idx, char player)
        {
            board[idx] = player;

            return board;
        }
        static string checkWin(char[] board)
        {
            int[,] winCombination = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            char[] players = new char[] { 'X', 'O' };
            string win = "no";
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[winCombination[j, 0]] == players[i] && board[winCombination[j, 1]] == players[i] && board[winCombination[j, 2]] == players[i])
                    {
                        if (players[i] == 'X')
                        {
                            win = "win";
                        }
                        else
                        {
                            win = "lose";
                        }
                    }
                }
            }

            if (getEmptySquare(board).Count == 0 && win == "no")
            {
                win = "tie";
            }

            return win;
        }

        static List<int> getEmptySquare(char[] board)
        {
            List<int> freeList = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    freeList.Add(i);
                }
            }

            return freeList;
        }
    }
}
