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
#include<cstdlib>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::qsort;

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

int binary_search(const vector<int>& stalls, const int& start, const int& end, const int& totalCows) {
	int minDistanceIndex = stalls[end];

	if (start < end) {
		int mid = start + (end - start - 1) / 2;	// Handle |YES|NO| situation where we will call binary_search with mid = x and end = x + 1 all the time.
		bool pResult = predicate(stalls, mid, totalCows);

		if (pResult) {
			minDistanceIndex = binary_search(stalls, mid, end, totalCows);
		}
		else {
			minDistanceIndex = binary_search(stalls, start, mid - 1, totalCows);
		}
	}	

	return stalls[minDistanceIndex] - stalls[0];
}

int compare(const void * a, const void * b)
{
	return (*(int*)a > *(int*)b);
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
		qsort(stalls.data(), stalls.size(), sizeof(int), compare);	
		cout << binary_search(stalls, 0, stalls.size() - 1, c) << endl;

		stalls.clear();
	}
}