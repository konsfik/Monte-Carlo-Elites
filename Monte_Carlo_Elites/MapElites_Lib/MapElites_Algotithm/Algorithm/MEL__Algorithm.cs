using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public class MEL__Algorithm<T>
        where T : MEL__Individual
    {
        // maze - related parameters
        public MEL__Operator_Settings<T> operator_settings;
        public MEL__Evaluation_Settings<T> evaluation_settings;
        public MEL__State<T> state;
        public MEL__Parent_Selection_Method<T> parent_selection_method;

        public MEL__Algorithm(
            MEL__Operator_Settings<T> operator_settings,
            MEL__Evaluation_Settings<T> evaluation_settings,
            MEL__Parent_Selection_Method<T> parent_selection_method
            )
        {
            this.operator_settings = operator_settings;
            this.evaluation_settings = evaluation_settings;
            this.parent_selection_method = parent_selection_method;

            this.state = new MEL__State<T>(
                evaluation_settings.feature_1_subdivisions,
                evaluation_settings.feature_2_subdivisions
                );
        }

        public void Generate_Initial_Population(
            Random randomness_provider,
            int initial_number_of_individuals
            )
        {
            List<T> new_individuals = new List<T>();

            for (int i = 0; i < initial_number_of_individuals; i++)
            {
                new_individuals.Add(
                    operator_settings.individual_generator.Generate_Individual(randomness_provider)
                    );
            }

            // evaluate all the new individuals
            foreach (var individual in new_individuals)
            {
                Evaluate_Invdividual(individual);
            }

            // place the individuals in their correct position, on the table
            foreach (var new_individual in new_individuals)
            {
                int x = Individual_Placement_Index_X(new_individual);
                int y = Individual_Placement_Index_Y(new_individual);

                if (
                    state.individuals[x, y] == null
                    ||
                    state.individuals[x, y].fitness < new_individual.fitness
                    )
                {
                    state.individuals[x, y] = new_individual;
                    state.individual_exists[x, y] = true;
                }
            }

        }

        public void Select_And_Mutate_Individual(Random randomness_provider)
        {
            // Select parent location (coordinates),
            // based on the assigned selection method:
            Vec2i parent_loc = parent_selection_method.Select_Parent_Coordinates(
                randomness_provider,
                this
                );

            T offspring =
                operator_settings.individual_mutator.Generate_Offspring(
                    randomness_provider,
                    state.individuals[parent_loc.x, parent_loc.y]
                    );

            Evaluate_Invdividual(offspring);

            // Increase the parent selections (as a location and as an individual) by one (1)
            state.selections__per__location[parent_loc.x, parent_loc.y] += 1;
            state.selections__per__individual[parent_loc.x, parent_loc.y] += 1;

            // Calculate the position (coordinates) of the new individual,
            // based on its features (feature_1, feature_2)
            Vec2i offspring_pos = Individual_Placement(offspring);

            // attempt to place this individual on the table of individuals...
            if (state.individuals[offspring_pos.x, offspring_pos.y] == null)
            {
                // DISCOVERY
                state.individual_exists[offspring_pos.x, offspring_pos.y] = true;

                // update the necessary tables:
                // - survival per (location & individual)
                // - discovery per (location & individual) **
                state.offspring_survivals__per__location[parent_loc.x, parent_loc.y] += 1;
                state.offspring_survivals__per__individual[parent_loc.x, parent_loc.y] += 1;

                // actually place the offspring at this new location
                state.individuals[offspring_pos.x, offspring_pos.y] = offspring;

                // for the new offspring, reset all its individual - parameters
                state.selections__per__individual[offspring_pos.x, offspring_pos.y] = 0;
                state.offspring_survivals__per__individual[offspring_pos.x, offspring_pos.y] = 0;
            }
            else if (offspring.fitness > state.individuals[offspring_pos.x, offspring_pos.y].fitness)
            {
                // REPLACED AN EXISTING INDIVIDUAL
                state.individual_exists[offspring_pos.x, offspring_pos.y] = true;

                // update the necessary tables:
                // - survival per (location & individual)
                // - replacement per (location & individual) **
                state.offspring_survivals__per__location[parent_loc.x, parent_loc.y] += 1;
                state.offspring_survivals__per__individual[parent_loc.x, parent_loc.y] += 1;

                // actually place the offspring at this new location
                state.individuals[offspring_pos.x, offspring_pos.y] = offspring;

                // for the new offspring, reset all its individual - parameters
                state.selections__per__individual[offspring_pos.x, offspring_pos.y] = 0;
                state.offspring_survivals__per__individual[offspring_pos.x, offspring_pos.y] = 0;
            }
        }

        private void Evaluate_Invdividual(T individual)
        {
            individual.fitness = evaluation_settings.fitness_calculator.Calculate_Fitness(individual);
            individual.feature_1 = evaluation_settings.feature_1_calculator.Calculate_Fitness(individual);
            individual.feature_2 = evaluation_settings.feature_2_calculator.Calculate_Fitness(individual);
        }


        private Vec2i Individual_Placement(MEL__Individual individual)
        {
            return new Vec2i(
                Individual_Placement_Index_X(individual),
                Individual_Placement_Index_Y(individual)
                );
        }
        protected int Individual_Placement_Index_X(MEL__Individual individual)
        {
            double feature_1_value = individual.feature_1;
            for (int i = 0; i < evaluation_settings.feature_1_subdivisions; i++)
            {
                if (feature_1_value < evaluation_settings.feature_1_value_ranges[i])
                {
                    return i;
                }
            }

            return evaluation_settings.feature_1_subdivisions - 1;
        }

        protected int Individual_Placement_Index_Y(MEL__Individual individual)
        {
            double feature_2_value = individual.feature_2;
            for (int i = 0; i < evaluation_settings.feature_2_subdivisions; i++)
            {
                if (feature_2_value < evaluation_settings.feature_2_value_ranges[i])
                {
                    return i;
                }
            }

            return evaluation_settings.feature_2_subdivisions - 1;
        }


        public double Q_Fitness_Sum()
        {
            int width = state.individuals.GetLength(0);
            int height = state.individuals.GetLength(1);

            double fitness_sum = 0.0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (state.individuals[x, y] != null)
                    {
                        fitness_sum += state.individuals[x, y].fitness;
                    }
                }
            }
            return fitness_sum;
        }

        public string Get_Settings_Description()
        {
            string description = "";
            description += "individual generator: " + operator_settings.individual_generator.ToString() + "\n";
            description += "individual mutator: " + operator_settings.individual_mutator.ToString() + "\n";
            description += "parent selection method: " + parent_selection_method.ToString() + "\n";
            description += "fitness calculation method: " + evaluation_settings.fitness_calculator.ToString() + "\n";
            description += "feature 1 calculation method: " + evaluation_settings.feature_1_calculator.ToString() + "\n";
            description += "feature 1 subdivisions: " + evaluation_settings.feature_1_subdivisions.ToString() + "\n";
            description += "feature 1 minimum value: " + evaluation_settings.feature_1_minimum_value.ToString() + "\n";
            description += "feature 1 maximum value: " + evaluation_settings.feature_1_maximum_value.ToString() + "\n";
            description += "feature 2 calculation method: " + evaluation_settings.feature_2_calculator.ToString() + "\n";
            description += "feature 2 subdivisions: " + evaluation_settings.feature_2_subdivisions.ToString() + "\n";
            description += "feature 2 minimum value: " + evaluation_settings.feature_2_minimum_value.ToString() + "\n";
            description += "feature 2 maximum value: " + evaluation_settings.feature_2_maximum_value.ToString() + "\n";
            return description;
        }
    }
}
