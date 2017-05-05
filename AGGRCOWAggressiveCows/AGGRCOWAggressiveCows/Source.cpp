/*
 * Hints: 
 *    https://www.quora.com/What-is-the-correct-approach-to-solve-the-SPOJ-problem-Aggressive-cow
 *    https://discuss.codechef.com/questions/77613/spoj-problem-aggrcow-aggressive-cows
 *    http://sahilmutneja.com/blog/2015/02/aggressive-cowsaggrcow-spoj/
 *    
 */
#include<iostream>
#include<vector>
#include<cstdlib>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::qsort;

// We use the array index median because the largest minimum distance (lmd) between two cows is the center point.
// For multiple cows, the lmd is the distance when the cows are spreaded evenly apart.
// Catch: the stall positions/locations are random and not necessarily the mid point.
int process(const vector<int>& stalls, const int& start, const int& end, const int& totalCows) {
	int minDistance = stalls[end] - stalls[0];
	
	bool increaseDistanceBetweenCows = false;

	if (start < end) {
		int mid = start + ((end - start + 1) / 2);

		int minimumDistanceCandidate = stalls[mid] - stalls[0];
		minDistance = minimumDistanceCandidate;

		int cowCount = 1;	// Starting count. First cow is always on the left most of the search space
		int runningDistance = 0;
		for (size_t i = 1; i < stalls.size(); i++) {
			// This is how far we are in total from the current stall, stalls[i], to the previous stall where we have placed a cow.
			runningDistance += stalls[i] - runningDistance;
			
			if (runningDistance - stalls[start] >= minimumDistanceCandidate) {
				cowCount++;
				runningDistance = stalls[i];
			}

			if (cowCount >= totalCows) {
				// We have put too little space between the cows. Need to increase the distance.
				increaseDistanceBetweenCows = true;
				break;
			}
		}
		
		// We want to find the minimum possible value where f(x), represented by stalls[mid] such that cowsRequired >= totalCows
		if (increaseDistanceBetweenCows) {
			// Need to increase the distance between the cows. Going right ensures a larger distance because the stalls are sorted so.
			// However, this could be a possible answer so we need to keep this in the search space.
			minDistance = process(stalls, mid, end, totalCows);
		}
		else {
			// This distance doesn't give enough space between cows. We cannot use it.
			minDistance = process(stalls, start, mid - 1, totalCows);
		}
	}

	return minDistance;
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
		cout << process(stalls, 0, stalls.size() - 1, c) << endl;

		stalls.clear();
	}
}
