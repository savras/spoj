/*
 * We use the array index median because the largest minimum distance (lmd) between two cows is the center point.
 * For multiple cows, the lmd is the distance when the cows are spreaded evenly apart.
 * Catch: the stall positions/locations are random and not necessarily the mid point.
 * So, for each of given the stall values, calculate the distances from stalls[0], then see how many
 * cows you can fit into the stalls array with that distance.
 *
 * Brute force: perform predicate() for each stall position, N, until you get cowCount == totalCow.
 * Hints: 
 *    https://www.quora.com/What-is-the-correct-approach-to-solve-the-SPOJ-problem-Aggressive-cow
 *    https://discuss.codechef.com/questions/77613/spoj-problem-aggrcow-aggressive-cows
 *    http://sahilmutneja.com/blog/2015/02/aggressive-cowsaggrcow-spoj/
 */
#include<iostream>
#include<vector>
#include<algorithm>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::sort;

//O(N), where you have as many cows as you have stalls, and mid is very small.
// Return true if we can increase the space between cows because we can place more cows than the number we need (totalCows),
// or decrease it if we don't have enough space to place the cows for the given number (totalCows)
bool predicate(const vector<int>& stalls, const int& mid, const int& totalCows) {
	int cowCount = 1;	// Starting count. First cow is always on the left most of the search space.
	int candidateDistance = stalls[mid] - stalls[0];		// Distance of stalls[mid] from 1st cow.
	int lastCowPosition = 0;		// The last stall to the right where we have last placed a cow.
	for (size_t i = 1; i < stalls.size(); i++) {

		// Find out how many cows can fit in the barn for each stall having 'candidate distance'
		// E.g. try putting a cow every candidateDistance away from each other, then find out if we can 
		// tweak (increase/decrease) the candidate distance
		if (stalls[i] - stalls[lastCowPosition] >= candidateDistance) {
			cowCount++;
			lastCowPosition = i;
		}

		if (cowCount >= totalCows)
		{
			// We can fit more cows than we have so the distance is not optimal.
			// We need to make the distance between each stall larger.
			// We still try and increase the distance when cowCount == totalCows
			// because the current candidateDistance may not be optimal,
			// and we are in the 'Y' region (left side of the array, |Y|Y|....Y|N|N|N|), but 
			// not necessarily the rightmost 'Y'. We want the rightmost 'Y'.
			return true;
		}
	}
	// Can't fit all the cows in the barn with the given stall distance.
	// We need to make the distance between the stalls smaller.
	// We are in the 'N' (right sight of the array, |Y|Y|....Y|N|N|N|).
	// Again, we want the leftmost 'Y'.
	return false;
}

// We will get an array like this |Y|Y|Y|Y|N|N|N|.
// What we want is the value in index 3, which is the rightmost Y.
// The rightmost Y represents the distance where all cows will be equally spaced apart (a.k.a maximum distance) 
// Y & N are represented by our predicate, which keeps tweaking the distances until we get the rightmost Y.
// Y == we can fit equal or greater number of cows in the stalls given the candidateDistance
// N == we cannot fit enough cows given the candidateDistance to meet the given totalCows number
// ToDo:: O(log X), where X is the number of positions in the stalls.. isn't X == N? I don't understand the question.
int binary_search(const vector<int>& stalls, int start, int end, const int& totalCows) {
	while (start < end) {
		const int handleRoundUp = 1;
		// Handle |YES|NO| situation where we will call binary_search with mid = x and end = x + 1 all the time.
		// By doing this, we will always select the right side of the |YES|NO| as the mid instead of the left.
		int mid = start + (end - start + handleRoundUp) / 2;
		bool canIncreaseCandidateDistance = predicate(stalls, mid, totalCows);

		if (canIncreaseCandidateDistance) {
			start = mid;
		}
		else {
			end = mid - 1;
		}
	}

	return stalls[start] - stalls[0];
}

int main() {
	int t, n, c;
	cin >> t;

	vector<int> result;

	vector<int> stalls;
	while (--t >= 0) {
		cin >> n >> c;
		for (size_t i = 0; i < n; i++) {
			int value;
			cin >> value;
			stalls.push_back(value);
		}
		sort(stalls.begin(), stalls.end());		// O(N log N)
		result.push_back(binary_search(stalls, 0, stalls.size() - 1, c));

		stalls.clear();
	}

	for (size_t i = 0; i < result.size(); i++) {
		cout << result[i] << endl;
	}
}

/*
Sample input:
6
10 3
1 2 9 8 4 4 8 9 2 1
5 3
1 2 8 4 9
20 3
9 8 7 10 6 5 4 3 2 1 19 18 17 16 15 14 13 12 11 20
3 3
0 1000000000 500000000
20 4
9 8 7 10 6 5 4 3 2 1 19 18 17 16 15 14 13 12 11 20
20 5
9 8 7 10 6 5 4 3 2 1 19 18 17 16 15 14 13 12 11 20

Output:
3
3
9
500000000
6
4
*/