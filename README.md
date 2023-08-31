# OOP_Assignment2
Hobby animals need several things to preserve their exhilaration. Steve has some hobby animals: tarantulas, 
hamsters, and cats. Every animal has a name and their exhilaration level is between 0 and 70 (0 means that the 
animals dies). If their keeper is joyful, he takes care of everything to cheer up his animals, and their exhilaration 
level increases: of the tarantulas by 1, of the hamsters by 2, and of the cats by 3.
On a usual day, Steve takes care of only the cats (their exhilaration level increases by 3), so the exhilaration level 
of the rest decreases: of the tarantulas by 2, and of the hamsters by 3. On a blue day, every animal becomes a bit 
sadder and their exhilaration level decreases: of the tarantulas by 3, of the hamsters by 5, of the cats by 7.
Steve’s mood improves by one if the exhilaration level of every alive animal is at least 5.
Every data is stored in a text file. The first line contains the number of animals. Each of the following lines contain 
the data of one animal: one character for the type (T – Tarantula, H – Hamster, C – Cat), name of the animal (one 
word), and the initial level of exhilaration.
In the last line, the daily moods of Steve are enumerated by a list of characters (j – joyful, u – usual, b – blue). The 
file is assumed to be correct.

#### List the animals of the highest exhilaration level at the end of each day.

A possible input file:
3
T Webster 20
H Butterscotch 30
C Cat-man-do 50
uuuujjbjbjuujj

