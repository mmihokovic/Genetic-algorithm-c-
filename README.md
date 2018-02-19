# Genetic-algorithm-c-
Genetic algorithm for optimizing a demo function

Genetic algorithm is a heuristic method of optimizing a problem by imitating the natural process of evolution. The similarities of this algorithm and evolution are the selection process and in the genetic operators. The mechanism of selection over a species of living beings in the evolutionary process makes the environment and conditions in nature. In genetic algorithms, the key to selection is the function of the target, which appropriately represents the problem to be solved. Similarly, as the environment and conditions in nature are the key to selection over a species of living beings, so the function of the key is the key to the selection of the population of the solution in the genetic algorithm. Namely, in the nature of an individual best suited to the conditions and environment in which he lives, the highest likelihood of survival and mating, and thus the transfer of his genetic material to his offspring. For a genetic algorithm, one solution is one item. Selection of good individuals is being transferred to the next population, and manipulation of genetic material creates new individuals. Such a cycle of selection, reproduction, and manipulation of the genetic material of the individual is repeated until the condition for stopping the evolution process is met. The ultimate result is the population of individuals (potential solutions). The best in the last iteration is the optimization solution.
Genetic algorithms to keep the population of constant individuals, such as the living organisms in the wild. Every new iteration of the genetic algorithm is a new population, crossing individuals from the previous population. A new population is mutated, and each new unit of the new population is evaluated. After the creation of a new population, the old one is deleted.

pseudocode:
def GenetichAlgorithm(crossoverProb, mutationProb, populationSize, iterations):
	population = createPopulation(populationSize)
	evaluate(population)
	while(True):
		newPopulation = None
		while(newPopulation.Lenght < populationSize):
			(par1, par2) = selectParents(population)
			(child1, child2) = krizaj(par1, par2)
			child1 = mutiraj(child2)
      child2 = mutiraj(child2)
newPopulation.Append(child1)
newPopulation.Append(child2)
		populacija = newPopulation
	break
return
