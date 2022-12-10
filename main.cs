using System;
using System.Collections.Generic;

class Program {
  public static void Follow(int headX, int headY, ref (int, int)[] tail) {
    int toFollowX = headX;
    int toFollowY = headY;

    for (int i=0; i<tail.Length;i++) {
      int tailX = tail[i].Item1;
      int tailY = tail[i].Item2;
      int dX = Math.Abs(toFollowX - tailX);
      int dY = Math.Abs(toFollowY - tailY);
      // If more than 1 step away, bring each dimension closer to the head
      if (dX > 1 || dY > 1) {
        // Chase along x
        if (toFollowX > tailX) {
          tailX++;
        } else if (toFollowX < tailX) {
          tailX--;
        }
        // Chase along y
        if (toFollowY > tailY) {
          tailY++;
        } else if (toFollowY < tailY) {
          tailY--;
        }
      }
      tail[i].Item1 = tailX;
      tail[i].Item2 = tailY;
      toFollowX = tailX;
      toFollowY = tailY;
    }

    //for (int i=0; i<tail.Length;i++) {
      //Console.WriteLine("Tail{0} = ({1},{2})", 8, tail[8].Item1, tail[8].Item2);
    //}
  }
  public static void MoveHead(char direction, int magnitude, ref int headX, ref int headY, ref HashSet<(int, int)> visited, ref (int, int)[] tail) {
    Console.WriteLine("Executing {0} by {1}", direction, magnitude);
    
    for (int i=0; i<magnitude; i++) {
      if (direction == 'L') {
        headX--;//=(Math.Max(headX-1,0));
      } else if (direction == 'R') {
        headX++;// = headX+1; //Math.Min(headX+1, 5);
      } else if (direction == 'U') {
        headY++;// = Math.Max(headY-1, 0);
      } else if (direction == 'D') {
        headY--;// = headY+1;//Math.Min(headY+1, 4);
      }
      //Console.WriteLine("HeadPos: ({0},{1})", headX, headY);
      Follow(headX, headY, ref tail);
      
      (int,int) coord = (tail[tail.Length-1].Item1,tail[tail.Length-1].Item2);
      int countBefore = visited.Count;
      visited.Add(coord);
      int countAfter = visited.Count;
      //Console.WriteLine("Adding ({0},{1}), from {2} to {3}", tailX,tailY,countBefore,countAfter);
    } 
    //Console.WriteLine("-------");
  }



  
  public static void Main (string[] args) {
    string[] moves = System.IO.File.ReadAllLines(@"moves.txt");
    int headX = 0;
    int headY = 0;
    int tailX = 0;
    int tailY = 0;
    (int, int)[] tail = {(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)};
    HashSet<(int,int)> visited = new HashSet<(int,int)>();
    
    foreach(string move in moves) {
      char direction = move[0];
      int magnitude = Convert.ToInt32(move.Substring(2, move.Length-2));
      MoveHead(direction, magnitude, ref headX, ref headY, ref visited, ref tail);
      
    }
    Console.WriteLine("HeadPos: ({0},{1})\tTailPos: ({2},{3})", headX, headY, tailX, tailY);

    int numVisited = visited.Count;
    Console.WriteLine("Visited {0}",numVisited);
  }
}