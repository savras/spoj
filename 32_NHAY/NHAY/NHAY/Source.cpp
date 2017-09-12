/*
 * KMP tutorial:
 *		1) Knuth-Morris-Pratt algorithm - https://www.youtube.com/watch?v=5i7oKodCRJo
 *		2) KMP simplified - https://www.youtube.com/watch?v=PvAngd1J5kw
 * DSA (Deterministic finite-state automata:
 *		1) Deterministic Finite Automata (Example -1) - https://www.youtube.com/watch?v=40i4PKpM0cI
 */
#include <iostream>
#include <string>
#include <vector>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::vector;

void buildDsa(string needle, vector<vector<int>>& dsa) {

}

void performKmp(string needle, string haystack, const vector<vector<int>>& dsa)
{

}

int main() {
	int n;
	cin >> n;

	string needle;
	cin >> needle;

	string haystack;
	cin >> haystack;

	vector<vector<int>> dsa(n);
	buildDsa(needle, dsa);
	performKmp(needle, haystack, dsa);

	return 0;
}