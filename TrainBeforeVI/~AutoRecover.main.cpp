#include <iostream>
#include <locale>
#include <cstdlib>
#include <fstream>
#include <vector>
#include <algorithm>


#include "Department.h"


int main()
{
	setlocale(LC_ALL, "RUS");

	auto input_file = std::ifstream("Text.txt");

	int middle_count_buc = 0;

	std::vector<Department> list_departments;

	auto buffer = std::string();

	if (input_file.is_open())
	{
		while (std::getline(input_file, buffer))
		{
			Department temp_dep(buffer);
			list_departments.push_back(temp_dep);
			middle_count_buc += temp_dep.GetCountBac();
		}
	}

	input_file.close();
	
	int ten_percent = middle_count_buc / 10;
	middle_count_buc /= list_departments.size();

	std::sort(list_departments.cbegin(), list_departments.cend());

	for each(auto dep in list_departments)
	{
		if (dep.GetCountBac() - middle_count_buc > ten_percent)
		{
			std::cout << dep << std::endl;
		}
	}

	system("pause");
	return 0;
}

std::ostream & operator<<(std::ostream & os, const Department data)
{
	return os << data.title << " " << data.zav <<" "<<data.count_bacalaures <<" "<< data.year << std::endl;
}
