/*
 * KMP tutorial:
 *		1) Knuth-Morris-Pratt algorithm - https://www.youtube.com/watch?v=5i7oKodCRJo
 *		2) KMP simplified - https://www.youtube.com/watch?v=PvAngd1J5kw
 * DSA (Deterministic finite-state automata) / Fail table/ Failure function:
 *		1) Deterministic Finite Automata (Example -1) - https://www.youtube.com/watch?v=40i4PKpM0cI
 * Proper prefix: All the characters in a string, with one or more cut off the end. �S�, �Sn�, �Sna�, and �Snap� are all the proper prefixes of �Snape�.
 * Proper suffix: All the characters in a string, with one or more cut off the beginning. �agrid�, �grid�, �rid�, �id�, and �d� are all proper suffixes of �Hagrid�.
 */
#include <iostream>
#include <string>
#include <vector>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::flush;
using std::vector;

void buildDfa(string needle, vector<int>& dfa) {
	dfa[0] = 0;

	int i = 0;	// Longest prefix.
	for (size_t j = 1; j < dfa.size(); j++) {
		while (i > 0 && needle[i] != needle[j]) {	// ToDo:: Don't understand reason behind this.
			i = dfa[i-1];
		}

		if (needle[i] == needle[j]) {
			i++;
		}
		dfa[j] = i;
	}
}

void performKmp(string needle, string haystack, const vector<int>& dfa, vector<int>& result)
{
	int i = 0;
	int jStart = 0;

	int j = 0;
	while (j < needle.size() && i < haystack.size()) {
		int runningI = i + j - jStart;
		if (needle[j] != haystack[runningI]) {
			if (j == 0) {
				i++;
			}
			else {
				j = dfa[j];
				i = runningI;
				jStart = j;
			}
		}
		else {
			j++;
		}

		// Found a needle. Print its starting index.
		if (j >= needle.size()) {
			result.push_back(i);
			i++;
			j = 0;
			jStart = j;
		}
	}
}

int main() {
	string line;
	std::getline(std::cin, line);
	while (line != "") {
		int n = stoi(line);

		string needle;
		cin >> needle;		

		string haystack;	// Read large string. haystrack pointer i keeps going forward so it doesn't matter.
		cin >> haystack;
		cin.ignore();

		vector<int> dfa(n);
		buildDfa(needle, dfa);

		vector<int> result;
		performKmp(needle, haystack, dfa, result);

		if (result.size() > 0) {
			for (size_t i = 0; i < result.size(); i++) {
				cout << result[i] << endl;
			}
		}
		else {
			cout << endl;
		}
		

		std::getline(std::cin, line);
	}

	return 0;
}