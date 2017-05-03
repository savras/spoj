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
// Catch: the stall positions/locations are random.
int process(const vector<int>& stalls, const int& start, const int& end, const int& totalCows) {
	if (start >= end) { return 0; }

	int mid = start + ((end - start) / 2);

	int minimumDistanceCandidate = stalls[mid];

	int cowCount = 1;	// Starting count.
	int runningDistance = 0;
	int max = -999999;
	for (size_t i = 1; i < stalls.size(); i++) {
		runningDistance += stalls[i] - runningDistance;
		if (runningDistance > max) { max = runningDistance; }

		if (runningDistance >= minimumDistanceCandidate) {
			cowCount++;
			runningDistance = 0;
		}

		if (cowCount >= totalCows) {
			break;
		}
	}

	if (cowCount < totalCows) {
		// Need to reduce the distance between the cows. Going left ensures a smaller distance because the stalls are sorted.
		// There is no need to try larger value. Remember that the maximum lmd is the mid point.
		 max = process(stalls, start, mid - 1, totalCows);
	}	

	return max;
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
	}
}
