/*
 * We use the array index median because the largest minimum distance (lmd) between two cows is the center point.
 * For multiple cows, the lmd is the distance when the cows are spreaded evenly apart.
 * Catch: the stall positions/locations are random and not necessarily the mid point.
 * So, for each of given the stall values, calculate the distances from stalls[0], then see how many
 * cows you can fit into the stalls array with that distance.
 *
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

bool predicate(const vector<int>& stalls, const int& mid, const int& totalCows) {
	int cowCount = 1;	// Starting count. First cow is always on the left most of the search space.
	int candidateDistance = stalls[mid] - stalls[0];		// Distance of stalls[mid] from 1st cow.
	int basePosition = 0;
	for (size_t i = 1; i < stalls.size(); i++) {
		if (stalls[i] - stalls[basePosition] >= candidateDistance) {
			cowCount++;
			basePosition = i;
		}

		if (cowCount >= totalCows)
		{
			return true;
		}
	}
	return false;
}

// We will get an array like this |Y|Y|Y|Y|N|N|N|.
// What we want is the value in index 3, which is the rightmost Y.
int binary_search(const vector<int>& stalls, int start, int end, const int& totalCows) {
	while (start < end) {
		const int handleRoundUp = 1;
		// Handle |YES|NO| situation where we will call binary_search with mid = x and end = x + 1 all the time.
		// By doing this, we will always select the right side of the |YES|NO| as the mid instead of the left.
		int mid = start + (end - start + handleRoundUp) / 2;
		bool pResult = predicate(stalls, mid, totalCows);

		if (pResult) {
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

	vector<int> stalls;
	while (--t >= 0) {
		cin >> n >> c;
		for (size_t i = 0; i < n; i++) {
			int value;
			cin >> value;
			stalls.push_back(value);
		}
		sort(stalls.begin(), stalls.end());	
		cout << binary_search(stalls, 0, stalls.size() - 1, c) << endl;

		stalls.clear();
	}
}

//int binary_search_recursive(const vector<int>& stalls, const int& start, const int& end, const int& totalCows) {
//	int minDistanceIndex = stalls[end];
//
//	if (start < end) {
//		const int handleRoundUp = 1;
//		// Handle |YES|NO| situation where we will call binary_search with mid = x and end = x + 1 all the time.
//		// By doing this, we will always select the right side of the |YES|NO| as the mid instead of the left.
//		int mid = start + (end - start + handleRoundUp) / 2;	
//		bool pResult = predicate(stalls, mid, totalCows);
//
//		if (pResult) {
//			minDistanceIndex = binary_search_recursive(stalls, mid, end, totalCows);
//		}
//		else {
//			minDistanceIndex = binary_search_recursive(stalls, start, mid - 1, totalCows);
//		}
//	}	
//
//	return stalls[minDistanceIndex] - stalls[0];
//}