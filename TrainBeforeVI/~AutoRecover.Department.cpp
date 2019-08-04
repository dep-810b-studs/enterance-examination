#include "Department.h"
#include <sstream>
#include <ostream>
#include <vector>
#include <functional>
#include <array>
#include <iterator>



Department::Department()
{
}

Department::Department(std::string _department_info)
{
	std::stringstream sstr{};
	sstr << _department_info;
	std::vector<std::string> vec{};

	{
		std::string temp{};
		while (sstr >> temp)
		{
			vec.push_back(std::move(temp));
		}
	}

	title = vec[0];
	zav = vec[1];
	count_bacalaures = std::stoi(vec[2]);
	year = std::stoi(vec[3]);

}

Department::Department(std::string _title, std::string _zav, int _count_bacalaures, int _year)
{
	title = _title;
	zav = _zav;
	count_bacalaures = _count_bacalaures;
	year = _year;
}

bool Department::operator > (const Department & dep)
{
	return this->title > dep.title;
}

Department::~Department()
{
}
