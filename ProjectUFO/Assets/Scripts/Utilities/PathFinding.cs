using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Game.Map;
using Assets.Scripts.Game;

namespace Assets.Scripts.Utilities
{
	public class PathFinding
	{
		public static List<Field> AStar (Field startField, Field goalField) //Forward heuristic as method argument. We might want to use that in future.
		{
			if (startField == goalField)
			{
				Debug.Log ("Could not calculate path between the same fields!");
				List<Field> result = new List<Field>();
				result.Add (startField);
				return result;
			}

			Field currentField = startField;

			HashSet<Field> visited = new HashSet<Field> ();
			HashSet<Field> opened = new HashSet<Field> (); 

			Dictionary<Field, double> gScores = new Dictionary<Field, double> ();
			Dictionary<Field, Field> cameFrom = new Dictionary<Field, Field>();

			PriorityQueue<Field> frontier = new PriorityQueue<Field> ();
			frontier.Push (currentField, CalculateHeuristic(currentField, goalField));
			opened.Add (currentField);

			gScores [currentField] = 0;

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
					Debug.Log ("goalField reached!");
					return GetPath(cameFrom, goalField, startField);
				}

				foreach (Field neighbour in currentField.Neighbours.Values)
				{

					if (!visited.Contains(neighbour))
					{
						double tentativeGScore = gScores[currentField] + 1;

						if (! opened.Contains(neighbour))
						{
							gScores [neighbour] = tentativeGScore;
							double tentativeCost = gScores[neighbour] + CalculateHeuristic (neighbour, goalField);
							opened.Add (neighbour);

							frontier.Push (neighbour, tentativeCost);

							cameFrom [neighbour] = currentField;
		
						}
						else if(tentativeGScore < gScores[neighbour])
						{
							gScores [neighbour] = tentativeGScore;
							double tentativeCost = gScores[neighbour] + CalculateHeuristic (neighbour, goalField);
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
				result.Add (previousField);
				currentField = previousField;

			}
			while(currentField != startField);

			result.Reverse ();
			return result;
		}

		public static int CalculateHeuristic(Field currentField, Field goalField)
		{
			Vector2 differenceVector = goalField.transform.position - currentField.transform.position;
			int heuristic = Math.Abs ((int)differenceVector.x) + Math.Abs ((int)differenceVector.y);
			return heuristic;
		}
	}
}
