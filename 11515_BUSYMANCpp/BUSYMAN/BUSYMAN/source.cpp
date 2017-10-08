/*
	Intuition: To maximize the selection, we choose the tasks that finish earliest, so that we 
			   have more time to fit other tasks into the day. Once we have selected the task that
			   finishes earliest (lets call this the current task), we need to find the next task that finishes 
			   earliest (after current task) but with start time that starts after current task.
	Source: CLRS Greedy algo topic 16.1
*/
#include <iostream>
#include <utility>
#include <vector>
#include <algorithm>

using std::endl;
using std::cout;
using std::cin;
using std::pair;
using std::vector;
using std::make_pair;

bool thisPairDoesNotEndTheSameTime(int endTime, vector<pair<int, int>> pairs, int i) {
	return pairs[i].first > endTime;
}

bool startTimeForThisPairIsGreaterOrEqualToEndTime(int endTime, vector<pair<int, int>> pairs, int i) {
	return pairs[i].second >= endTime;
}

int main() {
	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++) {
		int n;
		cin >> n;

		vector<pair<int, int>> pairs;
		for (size_t j = 0; j < n; j++) {
			int start;
			int end;
			cin >> start >> end;
			pairs.push_back(make_pair(end, start));	// End is first argument because pair automagically sorts by first property.
		}

		std::sort(pairs.begin(), pairs.end());	// Sort end time in ascending order

		int endTimeForCurrentTask = pairs[0].first;
		int count = 1;
		for (size_t i = 1; i < n; i++) {
			if (startTimeForThisPairIsGreaterOrEqualToEndTime(endTimeForCurrentTask, pairs, i) && 
				thisPairDoesNotEndTheSameTime(endTimeForCurrentTask, pairs, i)) {
				count++;
				int endTimeForCurrentTask = pairs[i].first;
			}
		}
		cout << count;
	}	

	return 0;
}