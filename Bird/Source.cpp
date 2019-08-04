#include <iostream>
#include <cstdlib>
#include <clocale>

#include "Birdie.h"


int main(int argc, char * argv[])
{
	setlocale(LC_ALL, "RUS");

	Birdie birdie(100, "Воробей","Пернатые");

	Birdie birdie2(birdie);
	Birdie birdie3;

	birdie3 = birdie;

	std::cout << birdie.GetWeight() << std::endl;
	std::cout << birdie.GetSpecies() << std::endl;
	std::cout << birdie.GetFamily() << std::endl;

	std::cout << birdie2.GetWeight() << std::endl;
	std::cout << birdie2.GetSpecies() << std::endl;
	std::cout << birdie2.GetFamily() << std::endl;

	std::cout << birdie3.GetWeight() << std::endl;
	std::cout << birdie3.GetSpecies() << std::endl;
	std::cout << birdie3.GetFamily() << std::endl;

	system("pause");

	return 0;
}