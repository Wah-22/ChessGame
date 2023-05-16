using System;
using System.Transactions;

namespace ChessGame

{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Initialize the chessboard
            char[,] chessboard = {
            {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'},
            {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
            {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'}
        };

            // Display the initial chessboard
            PrintChessboard(chessboard);
            bool gameEnded = false;
            string currentPlayer = "White";

            while (!gameEnded)
            {
                Console.WriteLine(currentPlayer + " player's turn");
                Console.Write("Enter the source position (e.g. a2): ");
                string sourcePosition = Console.ReadLine();
                Console.Write("Enter the target position (e.g. a4): ");
                string targetPosition = Console.ReadLine();

                int sourceRow = sourcePosition[1] - '1';
                int sourceCol = sourcePosition[0] - 'a';
                int targetRow = targetPosition[1] - '1';
                int targetCol = targetPosition[0] - 'a';

                char piece = chessboard[sourceRow, sourceCol];

                // Check if the move is valid
                if (IsValidMove(chessboard, sourceRow, sourceCol, targetRow, targetCol))
                {
                    // Move the piece
                    chessboard[targetRow, targetCol] = piece;
                    chessboard[sourceRow, sourceCol] = ' ';

                    // Switch the player
                    currentPlayer = (currentPlayer == "White") ? "Black" : "White";

                    // Display the updated chessboard
                    PrintChessboard(chessboard);
                }
                else
                {
                    Console.WriteLine("Invalid move! Try again.");
                }
            }
            Console.WriteLine("Game over!");
        }

        static void PrintChessboard(char[,] chessboard)
        {
            Console.WriteLine();
            Console.WriteLine("  a b c d e f g h");
            Console.WriteLine("  ----------------");
            for (int row = 0; row < 8; row++)
            {
                Console.Write(8 - row + " |");
                for (int col = 0; col < 8; col++)
                {
                    Console.Write(chessboard[row, col] + "|");
                }
                Console.WriteLine();
                Console.WriteLine("  ----------------");
            }
            Console.WriteLine();
        }

        static bool IsValidMove(char[,] chessboard, int sourceRow, int sourceCol, int targetRow, int targetCol)
        {
            char piece = chessboard[sourceRow, sourceCol];

            // Check if the target position is within the board
            if (targetRow < 0 || targetRow >= 8 || targetCol < 0 || targetCol >= 8)
                return false;

            // Check if the target position is occupied by a piece of the same color
            if (char.IsLower(piece) && char.IsLower(chessboard[targetRow, targetCol]))
                return false;

            if (char.IsUpper(piece) && char.IsUpper(chessboard[targetRow, targetCol]))
                return false;

            // Check the specific rules for each piece
            switch (char.ToLower(piece))
            {
                case 'r': // Rook
                    return sourceRow == targetRow || sourceCol == targetCol;

                case 'n': // Knight
                    int rowDiff = Math.Abs(targetRow - sourceRow);
                    int colDiff = Math.Abs(targetCol - sourceCol);
                    return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
                // ...continued



                case 'b': // Bishop
                    return Math.Abs(targetRow - sourceRow) == Math.Abs(targetCol - sourceCol);

                case 'q': // Queen
                    return (sourceRow == targetRow || sourceCol == targetCol) ||
                           (Math.Abs(targetRow - sourceRow) == Math.Abs(targetCol - sourceCol));

                case 'k': // King
                    return Math.Abs(targetRow - sourceRow) <= 1 && Math.Abs(targetCol - sourceCol) <= 1;

                case 'p': // Pawn
                    int rrowDiff = targetRow - sourceRow;
                    int ccolDiff = Math.Abs(targetCol - sourceCol);

                    if (char.IsUpper(piece))
                    {
                        // White Pawn
                        if (rrowDiff == -1 && ccolDiff == 0 && chessboard[targetRow, targetCol] == ' ')
                            return true;
                        else if (rrowDiff == -1 && ccolDiff == 1 && char.IsLower(chessboard[targetRow, targetCol]))
                            return true;
                        else if (sourceRow == 6 && rrowDiff == -2 && ccolDiff == 0 && chessboard[targetRow, targetCol] == ' ')
                            return true;
                    }
                    else
                    {
                        // Black Pawn
                        if (rrowDiff == 1 && ccolDiff == 0 && chessboard[targetRow, targetCol] == ' ')
                            return true;
                        else if (rrowDiff == 1 && ccolDiff == 1 && char.IsUpper(chessboard[targetRow, targetCol]))
                            return true;
                        else if (sourceRow == 1 && rrowDiff == 2 && ccolDiff == 0 && chessboard[targetRow, targetCol] == ' ')
                            return true;
                    }
                    break;
            }

            return false;
        }


    }
}
