#pragma once

class Birdie
{
private:
	double _weight;
	char _species[8];
	char * _family;
public:
	Birdie() {};
	Birdie(double, char [8], char *);
	Birdie(Birdie &);
	inline double GetWeight() const { return _weight; }
	inline char * GetSpecies() { return _species; }
	inline char * GetFamily() const { return _family; }

	Birdie & operator = (Birdie&);
	//~Birdie();
};