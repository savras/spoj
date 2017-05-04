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
// Catch: the stall positions/locations are random.
bool process(const vector<int>& stalls, const int& start, const int& end, const int& totalCows) {
	bool result = false;
	if (start >= end) { return result; }

	int mid = start + ((end - start) / 2);

	int minimumDistanceCandidate = stalls[mid];

	int cowCount = 1;	// Starting count. First cow is always on the left most of the search space
	int runningDistance = 0;
	for (size_t i = 1; i < stalls.size(); i++) {
		runningDistance += stalls[i] - runningDistance;

		if (runningDistance >= minimumDistanceCandidate) {
			cowCount++;
			runningDistance = 0;
		}

		if (cowCount >= totalCows) {
			result = true;
			break;
		}
	}

	return result;
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

		/*
		// We want to find the minimum possible value where f(x), represented by stalls[mid] such that cowsRequired >= totalCows
		if (cowCount < totalCows) {
		// Need to reduce the distance between the cows. Going left ensures a smaller distance because the stalls are sorted.
		minimumDistanceCandidate = process(stalls, start, mid - 1, totalCows);
		}
		else {
		minimumDistanceCandidate = process(stalls, mid + 1, end, totalCows);
		}

		return minimumDistanceCandidate;
		*/
		cout << process(stalls, 0, stalls.size() - 1, c) << endl;
	}
}
