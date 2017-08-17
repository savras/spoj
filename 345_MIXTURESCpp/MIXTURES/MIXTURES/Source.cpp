// Method:
// Fill bottom half of an  n * n array
// Moving one cell down == merging with left chemical, and 
// moving one cell to the left == merging with the right chemical
// Notice that when we finish filling the n * n array, we are actually also
// performing every single possible merge (trace this out with a small n.
// Every cell will contain the min of the mixture between the chemical to the left or the right.

// Comments saying we can use modified matrix chain multiplication (MCM)
#include <iostream>
#include <vector>
#include <algorithm>

using std::endl;
using std::cin;
using std::cout;
using std::min;
using std::vector;

int main() {
	int n;
	while (scanf("%d", &n) > 0) {		
		vector<int> chemicals;
		for (size_t j = 0; j < n; j++) {
			int value;
			cin >> value;
			chemicals.push_back(value);
		}

		if (n == 1) {
			cout << 0 << endl;
		}
		else {
			vector<vector<int>> mixture(n, vector<int>(n, -1));
			vector<vector<int>> smoke(n, vector<int>(n, -1));

			// Fill memoSum and memoMultiplication diagonally.
			for (size_t j = 1; j < n; j++)
			{
				int row = j - 1;
				for (int column = 0; column < n - j; column++)
				{
					row++;
					int smokeAmount = 0;
					if (mixture[row - 1][column] == -1 || mixture[row][column + 1] == -1)
					{
						mixture[row][column] = (chemicals[row] + chemicals[column]) % 100;
						smoke[row][column] = chemicals[row] * chemicals[column];
					}
					else
					{
						int mixtureTop = mixture[row - 1][column];
						int mixtureRight = mixture[row][column + 1];

						int moveDownValue = (mixtureTop + chemicals[row]) % 100;
						int moveLeftValue = (mixtureRight + chemicals[column]) % 100;
						if (moveDownValue <= moveLeftValue)
						{
							mixture[row][column] = moveDownValue;
						}
						else
						{
							mixture[row][column] = moveLeftValue;
						}

						smoke[row][column] = min(
							(mixtureTop * chemicals[row]) + smoke[row - 1][column],
							(mixtureRight * chemicals[column]) + smoke[row][column + 1]
						);
					}					
				}
			}
			cout << smoke[n - 1][0] << endl;
		}		
	}

	return 0;
}