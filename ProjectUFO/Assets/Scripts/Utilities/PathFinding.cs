﻿using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Game.Map;
//using Assets.Scripts.Game;
using Assets.Scripts.Game.Actors;

namespace Assets.Scripts.Utilities
{
	public class PathFinding
	{
		public static readonly int IMPASSABILITY_THRESHOLD = 10000;

		public delegate int Heuristic(Field currentField, Field goalField);

		public static List<Field> AStar (Field startField, Field goalField, Heuristic heuristic)
		{
			if (startField == goalField)
			{
				//Debug.Log ("Could not calculate path between the same fields!");
				List<Field> result = new List<Field>();
				result.Add (startField);
				return result;
			}

			Field currentField = startField;

			HashSet<Field> visited = new HashSet<Field> ();
			HashSet<Field> opened = new HashSet<Field> (); 

			Dictionary<Field, int> costsFromStart = new Dictionary<Field, int> ();
			Dictionary<Field, Field> cameFrom = new Dictionary<Field, Field>();

			PriorityQueue<Field> frontier = new PriorityQueue<Field> ();

			frontier.Push (currentField, heuristic(currentField, goalField));
			opened.Add (currentField);
			costsFromStart [currentField] = 0;

			while (frontier.Count > 1)
			{
				currentField = frontier.Pop();

				if (opened.Contains (currentField) == true)
				{
					opened.Remove (currentField);
				}
				else
				{
					continue;
				}

				visited.Add (currentField);

				if (currentField == goalField)
				{
					//Debug.Log ("goalField reached!"); Nobody cares!
					return GetPath(cameFrom, goalField, startField);
				}

				foreach (Field neighbour in currentField.Neighbours.Values)
				{

					if (!visited.Contains(neighbour))
					{
						int temporaryCostFromStart = costsFromStart[currentField] + CalculateCost(currentField, neighbour);//1;

						if ((! opened.Contains (neighbour)) || (temporaryCostFromStart < costsFromStart [neighbour]))
						{
							if (! opened.Contains (neighbour))
							{
								opened.Add (neighbour);
							}

							costsFromStart [neighbour] = temporaryCostFromStart;
							int tentativeCost = costsFromStart [neighbour] + heuristic (neighbour, goalField);
							frontier.Push (neighbour, tentativeCost);
							cameFrom [neighbour] = currentField;
						}
					}
				}
			}

			Debug.Log ("Goal field is unreachable!");
			return new List<Field>();
		}

		public static List<Field> GetPath(Dictionary<Field, Field> cameFrom, Field goalField, Field startField)
		{
			List<Field> result = new List<Field>();
			result.Add (goalField);
			Field previousField;
			Field currentField = goalField;

			do
			{
				cameFrom.TryGetValue (currentField, out previousField);

				if(CalculateCost(previousField, currentField) >= IMPASSABILITY_THRESHOLD)
				{
					result.Clear();
					Debug.Log("Impassable");
					return result;
				}

				result.Add (previousField);
				currentField = previousField;

			}
			while(currentField != startField);

			result.Reverse ();

			/*for (int i = 0; i < result.Count - 1; ++i)
			{
				if (CalculateCost(result[i], result[i + 1]) >= IMPASSABILITY_THRESHOLD)
				{
					result.Clear();
				}
			}*/

			return result;
		}

		public static int ManhattanHeuristic(Field currentField, Field goalField)
		{
			Vector2 differenceVector = goalField.transform.position - currentField.transform.position;
			int heuristic = Math.Abs ((int)differenceVector.x) + Math.Abs ((int)differenceVector.y);
			return heuristic;
		}

		public static int EmptyHeuristic(Field current, Field goal)
		{
			return 0;
		}


		public static int CalculateCost(Field current, Field next)
		{
			if (next.ContainsUnitOfType<Player>())
			{
				return IMPASSABILITY_THRESHOLD;
			}

			int cost = 1;
			foreach (Field neighbour in next.Neighbours.Values)
			{
				if (neighbour.ContainsUnitOfType<Player>())
					cost += 100;
			}
				
			return cost;
		}
	}
}
