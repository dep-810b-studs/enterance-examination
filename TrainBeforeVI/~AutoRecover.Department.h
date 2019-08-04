#include<string>

class Department
{
public:
	Department();
	Department(std::string);
	Department(std::string, std::string, int, int);
	inline std::string GetTitle() { return title; }
	inline std::string GetZav() { return zav; }
	inline int GetCountBac() { return count_bacalaures; }
	inline int GetYear() { return year; }
	bool operator >(const Department&);
	friend std::ostream& operator<<(std::ostream&, const Department);
	~Department();
private:
	std::string title;
	std::string zav;
	int count_bacalaures;
	int year;
};