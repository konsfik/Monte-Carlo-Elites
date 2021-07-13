# Monte Carlo Elites Framework
The __Monte Carlo Elites Framework__ was developed and used in the context of the __"Monte Carlo Elites: Quality-Diversity Selection as a Multi-Armed Bandit Problem"__ paper (Konstantinos Sfikas, Antonios Liapis and Georgios N. Yannakakis, in Proceedings of the Genetic and Evolutionary Computation Conference, 2021).
The provided source code can be used to reproduce the experiment results of the paper for further analysis, or to expand on the proposed methodology. 
This "readme" file serves primarily as a brief presentation of the proposed methodology and includes some extra visualizations that are not present in the relevant paper.
Furthermore, towards the end of this document one can find technical details about the structure of the provided source code as well as directions on how to use it.

You may find the full paper here: https://arxiv.org/pdf/2104.08781.pdf

Citation (BibTeX):
```
@inproceedings{sfikas2021montecarloelites,
	author={Konstantinos Sfikas and Antonios Liapis and Georgios N. Yannakakis},
	title={Monte Carlo Elites: Quality-Diversity Selection as a Multi-Armed Bandit Problem},
	booktitle={Proceedings of the Genetic and Evolutionary Computation Conference},
	year={2021},
}
```

# "Monte Carlo Elites: Quality-Diversity Selection as a Multi-Armed Bandit Problem" - Paper overview

## Introduction

In this paper we are treating the parent selection process of the MAP-Elites algorithm [[1]](#1) as a problem of exploitation vs exploration.
We address this problem through the  UCB formula [[2]](#2), a method which is commonly applied to tree search optimization.
Effectively, our method manages to increase the algorithm’s performance through a better resource allocation.

<table width="100%">
	<tr>
		<td colspan="2">
			<img src="/README_images/graphics/map_elites_explanation.png" title="UCB"/>
		</td>
    </tr>
	<tr>
		<td width="50%">
			<img src="/README_images/equations/UCB_formula.png" title="UCB"/>
		</td>
		<td>
			<img src="/README_images/graphics/decision_tree.png" title="Decision Tree"/>
		</td>
    </tr>
	<tr>
		<td colspan="2">
			<b>Figure 1.</b> Top: default operation of the MAP-Elites algorithm. Bottom - left: the UCB formula. Bottom - right: visual representation of a decision tree.
		</td>
    </tr>
	
</table>

## Methodology

### Offspring Survival as a Bias for Parent Selection
The basic premise of "Monte Carlo Elites" is that it replaces the random parent selection of MAP-Elites [[1]](#1).
Instead of that, it biases the selection, favoring individuals with a higher rate of offspring survival, in order to optimize the algorithm’s resource allocation.
The Curiosity Score [[3]](#3) is a preexisting method that also biases selection based on offspring survival.
However, our method is substantially different from the Curiosity Score, as it treats this bias in a rather different way and explores its relation to important aspects of the algorithm’s operation.

### Unique features of Monte-Carlo Elites
Apart from using the “offspring survival rate” as a selection bias, our proposed methodology includes three unique characteristics:
First of all, it examines the selection bias from two different perspectives: The “offspring survival rate” can refer either to the offspring of a specific individual, or to the offspring that have been produced from a specific cell of the MAP-Elites archive, whose contained individuals may change over time.
Second, our method emphasizes that parent selection can be perceived as a multi-armed bandit problem, where the exploitation of known, good parents must be balanced against the exploration for unknown, yet potentially good parents.
We are using the UCB formula [[2]](#2) as a way of addressing this problem, as it is one of the most extensively tested methods of its kind.
Finally, our method formulates a deterministic prioritization score which embeds both exploitation and exploration. Consequently it can be used directly, without the need for additional probabilistic methods such as tournament selection or roulette wheel selection. 

### UCB: Balancing Exploitation vs Exploration
Equation 1 explains how we are using the UCB formula in order to calculate a selection score for each individual or cell of the archive:
The left part of the formula expresses the “offspring survival rate” and promotes the exploitation of good parents.
The right part, on the other hand, has nothing to do with the bias and simply promotes the selection of parents that have not been tried out as much.
When the number of selections of an individual is zero, we treat its selection score as infinity, effectively giving a top priority to individuals that have never been selected.

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/equations/UCB_formula.png" title="UCB" width="75%"/>
		</td>
    </tr>
	<tr>
		<td>
			<b>Equation 1.</b> UCB - based selection bias. Where w(i) is the number of offspring survivals for  individual i, n(i) is the number of selections for i, Ns is the total number of selections and λ is a constant set to 1 / sqrt(2).
		</td>
    </tr>
</table>

### Methodological Variations
Since the balance between exploitation and exploration is such an important aspect of our methodology, we are also including the following two variations in the set of examined methods: First, as shown in Equation 2, we are testing exploitation on its own, so as to compare it with UCB. Second, as shown in Equation 3, we are testing exploration on its own. The exploration formula has been simplified but is practically equivalent to using the right part of the UCB formula. 

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/equations/Exploitation_Formula.png" title="Exploitation" width="50%"/>
		</td>
    </tr>
	<tr>
		<td>
			<b>Equation 2.</b> Exploitation - based selection bias. Where w(i) is the number of offspring survivals for  individual i and n(i) is the number of selections for i.
		</td>
    </tr>
</table>

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/equations/Exploration_Formula.png" title="Exploration" width="50%"/>
		</td>
    </tr>
	<tr>
		<td colspan="2">
			<b>Equation 3.</b> Exploration - based selection bias. Where n(i) is the number of selections for i.
		</td>
    </tr>
</table>

All three methods are tested in the two available variations (per individual or per cell), resulting to a set of six different approaches, as listed in table 1:

<table width="100%">
	<tr>
		<td colspan="2">
			<b>Table 1.</b> Methodological variations.
		</td>
    </tr>
	<tr><td><b>Ui:</b></td><td>individual - based UCB</td></tr>
	<tr><td><b>Uc:</b></td><td>cell - based UCB</td></tr>
	<tr><td><b>Ei:</b></td><td>individual - based exploitation-only</td></tr>
	<tr><td><b>Ec:</b></td><td>cell - based exploitation-only</td></tr>
	<tr><td><b>Xi:</b></td><td>individual - based eploration-only</td></tr>
	<tr><td><b>Xc:</b></td><td>cell - based eploration-only</td></tr>
</table>


### Baselines
Apart from the variations of our own method, which we just described, we are also testing against three different baselines, so that we can establish a comparative overview:
The first one is a greedy selection method which is based on the parent’s fitness.
The second one is the default, random selection method of the MAP-Elites algorithm [[1]](#1).
The third one is the Curiosity Score [[3]](#3), which also biases parent selection based on offspring survival, but treats this bias in a rather different way.
The methods' short-names are listed in the following table:

<table width="100%">
	<tr>
		<td colspan="2">
			<b>Table 2.</b> Baselines.
		</td>
    </tr>
	<tr><td><b>G:</b></td><td>Greedy (fitness - based)</td></tr>
	<tr><td><b>R:</b></td><td>Random</td></tr>
	<tr><td><b>C:</b></td><td>Curiosity Score [[3]](#3)</td></tr>
</table>

## Experiments

### Experiment 1: Rastrigin Function
The first experiment is based on the Rastrigin function, a mathematical formula that exhibits a large number of local optima which makes it ideal for testing quality diversity algorithms (shown in table 3, figure 2). We used the 6-dimensional version of this function, so the genotype is a vector of six real numbers.
The behavioral dimensions are simply the two first values of the genotype vector.

<table width="100%">
	<tr>
		<td colspan="2">
			<b>Table 3.</b> Formal problem definition for the Rastrigin test-bed.
		</td>
	</tr>
	<tr>
		<td width="25%">
			Genotype vector:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_1__rastrigin/equations/rastrigin__genotype.png" /> 
		</td>
	</tr>
	<tr>
		<td width="25%">
			Gene value domain:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_1__rastrigin/equations/rastrigin__gene_domain.png" /> 
		</td>
	</tr>
	<tr>
		<td width="25%">
			Fitness function (Rastrigin function):
		</td>
		<td width="75%">
			<img src="/README_images/testbed_1__rastrigin/equations/rastrigin__fitness.png" /> 
		</td>
	</tr>
	<tr>
		<td width="25%">
			Behavioral dimensions:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_1__rastrigin/equations/rastrigin__behavior.png" /> 
		</td>
  </tr>
</table>

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/testbed_1__rastrigin/rastrigin_function_3d_plot.png" title="Rastrigin" width="75%"/>
		</td>
    </tr>
	<tr>
		<td>
			<b>Figure 2.</b> 3D plot of the rastrigin function. Source: https://en.wikipedia.org/wiki/Rastrigin_function
		</td>
    </tr>
</table>

Figure 3 summarizes the results measured in the range between 10^3 and 10^6 evaluations, allowing us to observe the accumulation of quality and diversity in the archive, across time.
As we may see, there is a clear distinction between the UCB-based methods and the baselines.
Although they eventually converge to a maximum, the UCB based ones are rising faster and retaining this advantage across the whole range of generations.
UCB per individual, in specific, performed the best overall, however all six variations of our method performed better than the baselines in this experiment.

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/testbed_1__rastrigin/charts/Rastrigin_6D__log.png" title="rastrigin" />
		</td>
    </tr>
	<tr>
		<td>
			<b>Figure 3.</b> Rastrigin 6DoF: progression of performance metrics between 10^3 and 10^6 evaluations. Results are averaged from 100 runs; shaded areas show the 95% confidence interval.
		</td>
    </tr>
</table>

Figure 4 shows the accumulation of fitness in the archive, during a single run of the algorithm, for all tested selection methods.

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/Rastrigin_6D__fitness_heatmaps_single_run.png" title="rastrigin" />
		</td>
    </tr>
	<tr>
		<td>
			<b>Figure 4.</b> Rastrigin 6DoF: Fitness heat-maps for a single run, captured at 10^3, 3 * 10^3, 10^4, 3 * 10^4, 10^5, 3 * 10^5 and 10^6 evaluations.
		</td>
    </tr>
</table>

<table>
	<tr>
		<td>
			<img src="/README_images/gifs/Rastrigin_Animated_Heatmaps.gif" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td>
			<b>Figure 5: Rastrigin 6DoF:</b> Fitness heat-maps for a single run, animated.
		</td>
	</tr>
</table>

### Experiment 2: Arm Repertoire
The second experiment is based on the so-called "Arm-Repertoire" testbed, which is also commonly used for evaluating quality diversity algorithms.
The problem represents a simplified simulation of a robotic arm that has an arbitrary number of degrees of freedom. 
The genotype is a vector of real numbers, in our case 12, that represent the angles between consequent parts of the arm.
The objective is to minimize the variance between those angles and the behavioral dimensions are the resulting coordinates of the end-point of the arm, on the 2D plane.

<table width="100%">
	<tr>
		<td colspan="2">
			<b>Table 4.</b> Formal problem definition for the "Arm Repertoire" test-bed.
		</td>
	</tr>
	<tr>
		<td width="25%">
			Genotype vector:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_2__arm_repertoire/equations/arm_repertoire__genotype.png" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td width="25%">
			Gene value domain:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_2__arm_repertoire/equations/arm_repertoire__gene_domain.png" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td width="25%">
			Fitness function:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_2__arm_repertoire/equations/arm_repertoire__fitness.png" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td width="25%">
			Behavior:
		</td>
		<td width="75%">
			<img src="/README_images/testbed_2__arm_repertoire/equations/arm_repertoire__behaviour.png" title="rastrigin"/> 
		</td>
  </tr>
</table>

<table width="100%">
	<tr>
		<td>
			<img src="/README_images/testbed_2__arm_repertoire/arm_repertoire_graphic.png"  width="35%"/>
		</td>
    </tr>
	<tr>
		<td>
			<b>Figure 6.</b> Arm Repertoire: representation of the robotic arm, explaining what the genotype (vector of angles) and behavior (end-point of the arm).
		</td>
    </tr>
</table>

As the results showcase, the UCB – based methods offer a significant performance advantage over the baselines.
In this example it is the UCB per cell method that performs best, and not the UCB per individual, as in the previous example.
However, similar to the previous problem, all variations of our proposed method are performing better than all the baselines.

<!-- Arm Repertoire 12D: performance charts -->
<table>
	<tr>
		<td>
			<img src="/README_images/testbed_2__arm_repertoire/charts/Arm_12D__log.png" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td>
			<b>Figure 7: Arm Repertoire 12DoF:</b> progression of performance metrics between 10^3 and 10^6 evaluations. Results areaveraged from 100 runs; shaded areas show the 95% confidence interval.
		</td>
	</tr>
</table>

By observing the accumulation of fitness in the archive, during a single run, we can, once again, see the different behavior of the examined selection methods.
In this case, the UCB per cell method manages to establish a good coverage much faster than all other methods, as is clearly seen at 10000 evaluations.
Similar to the previous example, given enough time, all methods converge to a very similar state.

<table>
	<tr>
		<td>
			<img src="/README_images/Arm_12D__fitness_heatmaps_single_run.png" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td>
			<b>Figure 8: Arm Repertoire 12DoF:</b> Fitness heat-maps for a single run, captured at 10^3, 3 * 10^3, 10^4, 3 * 10^4, 10^5, 3 * 10^5 and 10^6 evaluations.
		</td>
	</tr>
</table>

<table>
	<tr>
		<td>
			<img src="/README_images/gifs/Arm_Repertoire_Animated_Heatmaps.gif" title="rastrigin" /> 
		</td>
	</tr>
	<tr>
		<td>
			<b>Figure 9: Arm Repertoire 12DoF:</b> Fitness heat-maps for a single run, animated.
		</td>
	</tr>
</table>



### Experiment 3: Maze Generation 
The third experiment is based on a maze - generation problem (an example is shown in figure 5) whose purpose is to bring our method in the context of Procedural Content Generation.
For this problem we are using five behavioral characterizations, including various types of symmetry, as well as various topological characteristics of the generated mazes.
Those five features are tested interchangeably as either behavioral dimensions, or the objective, resulting to a set of 30 variations of the same problem.

<table width="100%">
	<tr>
		<td width="33%">
			<figure>
				<img src="/README_images/testbed_3__maze_generation/mutation_process/maze_1_2__init_state_solution.png">
				<figcaption>(a) Initial state & solution</figcaption>
			</figure>
		</td>
		<td width="33%">
			<figure>
				<img src="/README_images/testbed_3__maze_generation/mutation_process/maze_2_1__destroyed_cells.png">
				<figcaption>(b) Mutation: destroyed cells</figcaption>
			</figure>
		</td>
		<td width="33%">
			<figure>
				<img src="/README_images/testbed_3__maze_generation/mutation_process/maze_3_1__reconnected_cells.png">
				<figcaption>(c) Repair step 1: reconnecting cells with RDFS</figcaption>
			</figure>
		</td>
	</tr>
	<tr>
		<td width="33%">
			<figure>
				<img src="/README_images/testbed_3__maze_generation/mutation_process/maze_3_2__islands.png">
				<figcaption>(d) Disconnected islands</figcaption>
			</figure>
		</td>
		<td width="33%">
			<figure>
				<img src="/README_images/testbed_3__maze_generation/mutation_process/maze_4_1__reconnected_islands.png">
				<figcaption>(e) Repair step 2: reconnecting islands</figcaption>
			</figure>
		</td>
		<td width="33%">
			<figure>
				<img src="/README_images/testbed_3__maze_generation/mutation_process/maze_4_2__final_state_solution.png">
				<figcaption>(f) Final state & solution</figcaption>
			</figure>
		</td>
	</tr>
	<tr>
		<td colspan="3">
			<b>Figure 8.</b> Generation process of a single maze.
		</td>
	</tr>
</table>

<table width="100%">
	<tr>
		<td width="100%" colspan=2>
			<b>Table 5.</b> Behavioral characterizations / objectives for the maze generation test-bed.
		</td>
	</tr>
	<tr>
		<td width="50%">
			<b>Fitness 1: Percentage of corners</b> (L-shaped maze cells) is calculated via the equation on the right, where n is the number of cells of the maze, c is a specific cell and L(c) is a function that returns 1 if that cell is a corner, or 0 otherwise.
		</td>
		<td width="50%">
			<img src="/README_images/testbed_3__maze_generation/equations/mazes__fitness_1__corners.png" /> 
		</td>
	</tr>
	<tr>
		<td width="50%">
			<b>Fitness 2: Percentage of corridors</b> (I-shaped maze cells) is calculated via the equation on the right, where n is the number of cells of the maze, c is a specific cell and I(c) is a function that returns 1 if that cell is a corridor, or 0 otherwise.
		</td>
		<td width="50%">
			<img src="/README_images/testbed_3__maze_generation/equations/mazes__fitness_2__corridors.png" /> 
		</td>
	</tr>
	<tr>
		<td width="50%">
			<b>Fitness 3: Horizontal Symmetry</b> is calculated via the equation on the right, where n is the number of cells of the maze, c is a specific cell and S_H(c) is a function that returns a fraction of similarity of cell c with its horizontally symmetrical cell c'.
		</td>
		<td width="50%">
			<img src="/README_images/testbed_3__maze_generation/equations/mazes__fitness_3__horizontal_symmetry.png" /> 
		</td>
	</tr>
	<tr>
		<td width="50%">
			<b>Fitness 4: Horizontal And Vertical Symmetry</b> is calculated via the equation on the right, where n is the number of cells of the maze, c is a specific cell, S_H(c) is a function that returns a fraction of similarity of cell c with its horizontally symmetrical cell c' and S_V(c) is a function that returns a fraction of similarity of cell c with its vertically symmetrical cell c''.
		</td>
		<td width="50%">
			<img src="/README_images/testbed_3__maze_generation/equations/mazes__fitness_4__horizontal_and_vertical_symmetry.png" /> 
		</td>
	</tr>
	<tr>
		<td width="50%">
			<b>Fitness 5: Medium Solution Length</b> is a score that promotes mazes whose solution length (i.e. the length path for getting from the top-left to the bottom-right corner) is half of the total number of maze cells. It is calculated via the equation on the right, where n is the number of cells of the maze, c is a specific cell, P is the length of the solution path ant T is the total number of cells.
		</td>
		<td width="50%">
			<img src="/README_images/testbed_3__maze_generation/equations/mazes__fitness_5__medium_solution_path.png" /> 
		</td>
	</tr>
</table>

The best performing method in this experiment was UCB per cell, similar to the previous one.
Interestingly, however, in this problem the Curiosity Score performed much better than in the previous ones, managing to surpass the performance of UCB per individual.
Finally, both UCB based methods, as well as the exploration methods still performed better than the default, random parent selection.

<table>
	<tr>
		<td>
			<img src="/README_images/testbed_3__maze_generation/charts/Mazes__log.png" /> 
		</td>
	</tr>
	<tr>
		<td>
			<b>Figure 9: Maze generation:</b> progression of performance metrics between 10^3 and 10^6 evaluations, averaged across 100 runs for all variations of the experiment; shaded areas show the 95% confidence interval.
		</td>
	</tr>
</table>

Although our paper mainly focuses on an algorithmic optimization,
we should point out that in the context of procedural content generation, MAP-Elites can also be used as a form of expressivity analysis [[4]](#4).
In this example we may see the distribution of fitness along the behavioral dimensions of horizontal symmetry and percentage of corners, as well as a some indicative examples of mazes generated by the algorithm.

<table>
	<tr>
		<td>
			<img src="/README_images/testbed_3__maze_generation/fitness_heatmaps/mazes_expressivity.png" /> 
		</td>
	</tr>
	<tr>
		<td>
			<b>Figure 9: Maze generation:</b> progression of performance metrics between 10^3 and 10^6 evaluations, averaged across 100 runs for all variations of the experiment; shaded areas show the 95% confidence interval.
		</td>
	</tr>
</table>

### Experiment results - summary:
As a short summary of the experiment results, we would like to point out three key observations.
The first one is that the UCB-based selection methods offer a consistent performance advantage over all baselines and across all test-beds.
The second one is that the proper selection between the “per individual” and “per cell” variations seems to be a problem specific option.
The third one is that the exploration – only variants yield a better coverage but not as good quality as UCB, while the reverse is true for the exploitation – only variants.

## Concluding remarks:
To conclude, let’s point out the most important contributions of this paper.
First of all, in this paper we framed parent selection in the context of MAP-Elites as a problem of exploitation vs exploration.
Furthermore, we proposed that the exploitation component should be based on an indirect property of the individuals, which is the survival rate of their offspring.
Finally, we introduced and tested two different ways of measuring offspring survival (per individual or per cell) and found out that the cell-based approaches are more robust and perform better in most cases.
Overall, our proposed methodology offers a significant performance advantage over the default MAP-Elites algorithm and showcases a new path for the optimization of QD algorithms in general.

## References
<a id="1">[1]</a> 
Jean-Baptiste Mouret and Jeff Clune. 2015. Illuminating search spaces by mapping
elites. ArXiv abs/1504.04909 (2015).

<a id="2">[2]</a> 
Cameron Browne, Edward Powley, Daniel Whitehouse, Simon Lucas, Peter I. Cowling, Philipp Rohlfshagen, Stephen Tavener, Diego Perez, Spyridon Samothrakis, and Simon Colton. 2012. A Survey of Monte Carlo Tree Search Methods. IEEE Transactions on Computational Intelligence and AI in Games 4 (2012), 1–43.

<a id="3">[3]</a> 
Antoine Cully and Yannis Demiris. 2018. Quality and Diversity Optimization: A Unifying Modular Framework. IEEE Transactions on Evolutionary Computation 22, 2 (2018), 245–259.

<a id="4">[4]</a> 
Daniele Gravina, Ahmed Khalifa, Antonios Liapis, Julian Togelius and Georgios N. Yannakakis: “Procedural Content Generation through Quality-Diversity,” in Proceedings of the IEEE Conference on Games, 2019.

# Framework overview:
The underlying experimental framework has been developed in c# (.Net Framework 4.8) using Visual Studio 2019.
The visual studio solution consists of a number of interconnected projects, each of which has a specific role, as explained in the following table:

## Project Structure
<table width="100%">
	<tr>
		<td width="30%">
			<b>Project name / link to folder</b>
		</td>
		<td width="70%">
			<b>Project description</b>
		</td>
	</tr>
	<tr>
		<td width="30%">
			<!-- remember to change the relative link when this is inserted in the public repository -->
			<a href="/Monte_Carlo_Elites/MapElites_Lib">MAP_Elites_Lib</a>
		</td>
		<td width="70%">
			This <b>library project</b> is an abstract implementation of the MAP-Elites algorithm that operates with 2 feature dimensions. 
			The library includes all of the proposed parent selection methods (Ui, Uc, Ei, Ec, Xi, Xc, G, R, C).
			The library has been implemented so as to facilitate extensibility, at least in the aspects of interest of the relevant research.
			It is quite straightforwrd to implement different parent selection methods, by simply overriding the parent class.
			Furthermore, the library is not bound to any specific problem, but is implemented concretely for each one of the three test-beds in separate projects (<a href="/Monte_Carlo_Elites/Testbed_1__Rastrigin">Testbed_1__Rastrigin</a>, <a href="/Monte_Carlo_Elites/Testbed_2__Arm">Testbed_2__Arm</a>, <a href="/Monte_Carlo_Elites/Testbed_3__Mazes">Testbed_3__Mazes</a>).
			Consequently, one may easily apply the selected methodologies to other problems, by implementing a relatively small number of classes.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Testbed_1__Rastrigin">Testbed_1__Rastrigin</a>
		</td>
		<td width="70%">
			This <b>library project</b> is a concrete implementation of the MAP-Elites algorithm for the Rastrigin test-bed.
			Namely, this library references the MAP_Elites_Lib project and concretely implements the definition of an Individual, a Generation method, a Mutation method, as well as the fitness function and the feature space evaluation methods, according to the specified settings, mentioned in previous sections.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Testbed_1__Rastrigin__Exp">Testbed_1__Rastrigin__Exp</a>
		</td>
		<td width="70%">
			This <b>console application</b> can be used to reproduce the experiments of the Rastrigin test-bed.
			The application will save the relevant data (mainly in the form of csv files) under the executable's local directory, so that it can be completely portable and not reliant to specific folder paths.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Testbed_2__Arm">Testbed_2__Arm</a>
		</td>
		<td width="70%">
			This <b>library project</b> is a concrete implementation of the MAP-Elites algorithm for the Arm Repertoire test-bed.
			Similar to the <a href="/Monte_Carlo_Elites/Testbed_1__Rastrigin">Testbed_1__Rastrigin</a> project, this one also references the <a href="/Monte_Carlo_Elites/MapElites_Lib">MAP_Elites_Lib</a> library and implements the missing parts.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Testbed_2__Arm__Exp">Testbed_2__Arm__Exp</a>
		</td>
		<td width="70%">
			A <b>console application</b> that can reproduce the experiments of the Arm Repertoire test-bed.
			The application will save the relevant data (mainly in the form of csv files) under the executable's local directory, so that it can be completely portable and not reliant to specific folder paths.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Testbed_3__Mazes">Testbed_3__Mazes</a>
		</td>
		<td width="70%">
			This <b>library project</b> is a concrete implementation of the MAP-Elites algorithm for the Maze Generation test-bed.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Testbed_3__Mazes__Exp">Testbed_3__Mazes__Exp</a>
		</td>
		<td width="70%">
			This <b>console application</b> can be used to reproduce the experiments of the Maze Generation test-bed.
			The application will save the relevant data (mainly in the form of csv files) under the executable's local directory, so that it can be completely portable and not reliant to specific folder paths.
		</td>
	</tr>
	<tr>
		<td width="30%">
			<a href="/Monte_Carlo_Elites/Perfect_Mazes_Lib">Perfect_Mazes_Lib</a>
		</td>
		<td width="70%">
			This <b>library project</b> contains all the necessary functionality for generating, mutating and evaluating "perfect mazes".
			It contains the definition of a Random Depth First Search (RDFS) algorithm for the generation and repair of mazes, 
			the definition of the mutation method, as well as the definition for all of the evaluation methods that have been used in the paper.
			It has been primarily developed as a stand-alone library, so that it may be used in the context of other algorithms.
		</td>
	</tr>
	<tr>
		<td width="30%">
			Perfect_Mazes_Lib
		</td>
		<td width="70%">
			A library project that contains all the necessary functionality for generating, mutating and evaluating "perfect mazes".
		</td>
	</tr>
</table>

## How to reproduce the experiment results:
If the project description seems too detailed and you just need to get started, you may follow this small tutorial.

1. Download the source-code.
2. Extract the zip file at your preferred location.
3. Open the solution file () with Visual Studio.
4. There are three executable projects in the solution (<a href="/Monte_Carlo_Elites/Testbed_1__Rastrigin">Testbed_1__Rastrigin</a>, <a href="/Monte_Carlo_Elites/Testbed_2__Arm">Testbed_2__Arm</a> and <a href="/Monte_Carlo_Elites/Testbed_3__Mazes">Testbed_3__Mazes</a>), each of which will allow you to run the experiments related to one of the three testbeds. The results will be saved in a new folder, next to the location of the corresponding executable.







