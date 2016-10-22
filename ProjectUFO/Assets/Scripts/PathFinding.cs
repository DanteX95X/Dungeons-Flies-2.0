using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Game.Map;
using Assets.Scripts.Game;
using Assets.Scripts.Utilities;

namespace Assets.Scripts
{
	public class PathFinding
	{
		public static List<Field> AStar (Field startField, Field goalField)
		{
			if (startField == goalField)
			{
				Debug.Log ("Could not calculate path between the same fields!");
				return new List<Field> ();
			}

			#region variables

			Field currentField = startField;

			HashSet<Field> visited = new HashSet<Field> ();
			HashSet<Field> notVisited = new HashSet<Field> ();

			Dictionary<Field, double> costs = new Dictionary<Field, double> ();
			Dictionary<Field, Field> cameFrom = new Dictionary<Field, Field>();

			PriorityQueue<Field> frontier = new PriorityQueue<Field> ();

			#endregion

			frontier.Push (currentField, 0);
			costs [currentField] = 0;

			while (visited.Count != 20)
			{
				currentField = frontier.Pop();
				visited.Add (currentField);

				if (currentField == goalField)
				{
					Debug.Log ("goalField reached!");
					return GetPath(cameFrom, goalField, startField);
				}

				foreach (Field neighbour in currentField.Neighbours.Values)
				{
					if (!visited.Contains(neighbour))
					{
						double tentativeCost = costs [currentField] + 1 + CalculateHeuristic (neighbour, goalField);
						bool tentativeIsBetter = false;

						if (! notVisited.Contains (neighbour))
						{
							notVisited.Add (neighbour);
							tentativeIsBetter = true;
						}
						else if(tentativeCost < costs[neighbour])
						{
							tentativeIsBetter = true;
						}

						if (tentativeIsBetter == true)
						{
							cameFrom [neighbour] = currentField;
							costs [neighbour] = tentativeCost;
							frontier.Push (neighbour, costs[neighbour]);
						}

					}
				}

			}

			return new List<Field>();

		}

		public static List<Field> GetPath(Dictionary<Field, Field> cameFrom, Field goalField, Field startField)
		{
			List<Field> result = new List<Field>();
			result.Add (goalField);
			Field previousField;
			Field currentField = goalField;

			do {
				cameFrom.TryGetValue (currentField, out previousField);
				Debug.Log (previousField.transform.position.x + " " + previousField.transform.position.y);
				result.Add (previousField);
				currentField = previousField;

			} while(currentField != startField);

			result.Add(startField);
			result.Reverse ();

			return result;

		}

		public static double CalculateHeuristic(Field currentField, Field goalField)
		{
			Vector2 differenceVector = goalField.transform.position - currentField.transform.position;
			double heuristic = differenceVector.magnitude;
			Debug.Log ("Heuristic: " + heuristic);
			return heuristic;
		}

	}

}