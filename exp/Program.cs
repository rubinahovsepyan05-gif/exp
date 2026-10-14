namespace exp;
enum Player
{
    Empty=0,
    x,
    o
}
class Program
{
    static bool TryReadPlayer(out Player player)
    {
        Console.WriteLine("Which player are you? x or o/1 or 2:");
        string playerName = Console.ReadLine();
        player = Player.Empty;
        if (int.TryParse(playerName, out int playerNumber))
        {
            if (playerNumber == 1 || playerNumber == 2)
            {
                player = (Player)playerNumber;
            }
            else
            {
                Console.WriteLine("Please enter 1 or 2");
                return false;
            }
        }
        else
        {
            if (playerName.ToLower() == "x")
            {
                player = Player.x;
            }
            else if (playerName.ToLower() == "o")
            {
                player = Player.o;
            }
            else
            {
                Console.WriteLine("Please enter x or o");
                return false;
            }
        }

        return true;
    }

    struct Move
    {
        public int row;
        public int col;

        public Move(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
    static Move ReadMove(Player[,] board){
        while (true)
        {
            int row = ReadNumber("Row");
            int col= ReadNumber("Column");
            if (row >= 1 && row <= 3 && col >= 1 && col <= 3 && board[row - 1, col - 1] == Player.Empty)
            {
                return new Move(row - 1, col - 1);
            }
            Console.WriteLine("Pick an empty cell");
        }
    }

    static Player Winner(Player[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if(Line(board[i,0],board[i,1],board[i,2]))
            {
                return board[i,0];
            }
            if (Line(board[0,i],board[1,i],board[2,i]))
            {
                return board[0, i];
            }
        }

        if (Line(board[0, 0], board[1, 1], board[2, 2]))
        {
            return board[0, 0];
        }

        if (Line(board[0, 2], board[1, 1], board[2, 0]))
        {
            return board[0, 2];
        }
        return Player.Empty;
    }
    static int ReadNumber(string name)
    {
        while (true)
        {
            Console.Write($"{name}: ");
            string input = Console.ReadLine();
            int number;
            if (input != null && int.TryParse(input, out number))
            {
                return number;
            }
            Console.WriteLine("Enter a whole number.");
        }
    }

    static bool Line(Player a, Player b, Player c)
    {
        if (a != Player.Empty && a == b && a == c)
        {
            return true;
        }
        return false;
    }

    static void PrintBoard(Player[,] board)
    {
        Console.WriteLine("  1 2 3");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(Mark(board[i, j]) + " ");
            }
            Console.WriteLine();
        }
    }

    static char Mark(Player player)
    {
        if (player == Player.x)
        {
            return 'x';
        }

        if (player == Player.o)
        {
            return 'o';
        }
        return '.';
    }
    
    static void Main(string[] args)
    {
        Player[,] board = new Player[3, 3];

        Player currentPlayer;

        while (true)
        {
            if (!TryReadPlayer(out currentPlayer))
            {
                continue;
            }

            break;
        }

        for (int turn = 0; turn < 9; turn++)
        {
            PrintBoard(board);

            Console.WriteLine($"Player {Mark(currentPlayer)}'s turn");

            Move move = ReadMove(board);

            board[move.row, move.col] = currentPlayer;

            Player winner = Winner(board);

            if (winner != Player.Empty)
            {
                PrintBoard(board);
                Console.WriteLine($"Player {Mark(winner)} wins!");
                return;
            }

            bool full = true;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == Player.Empty)
                    {
                        full = false;
                    }
                }
            }

            if (full)
            {
                PrintBoard(board);
                Console.WriteLine("Draw!");
                return;
            }

            currentPlayer = currentPlayer == Player.x
                ? Player.o
                : Player.x;
        }
    }
}