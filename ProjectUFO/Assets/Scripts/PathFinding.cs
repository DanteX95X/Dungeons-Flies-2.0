using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Game.Map;
using Assets.Scripts.Game;

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

			//#region variables

			Field currentField = startField;
		//	Field goalField = Level.Grid [goalPosition];

			HashSet<Field> visited = new HashSet<Field> ();
			HashSet<Field> notVisited = new HashSet<Field> ();

			Dictionary<Field, double> costs = new Dictionary<Field, double> ();
			Dictionary<Field, Field> cameFrom = new Dictionary<Field, Field>();

			PriorityQueue<Field> frontier = new PriorityQueue<Field> ();

			//#endregion

			frontier.Push (currentField, 0);
			costs [currentField] = 0;

			while (visited.Count != 20)
			{
				currentField = frontier.Pop();
				visited.Add (currentField);

				if (currentField == goalField)
				{
					Debug.Log ("goalField reached!");
					Debug.Log ("Position: " + currentField.transform.position.x + " " + currentField.transform.position.y);
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

						//Debug.Log ("Sasiad: " + neighbour.transform.position.x + " " + neighbour.transform.position.y);
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

	public class Pair<T1, T2>
	{
		public T1 first { get; set; }
		public T2 second { get; set; }


		public Pair()
		{

		}

		public Pair(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}

	}

	public class PriorityQueue<T>
	{
		List<Pair<T, double>> nodes { get; set; }

		public int Count { get { return nodes.Count; } }

		public Pair<T, double> Top
		{
			get
			{
				if (nodes.Count < 2)
					throw new IndexOutOfRangeException();
				return nodes[1];
			}
		}

		public PriorityQueue()
		{
			nodes = new List<Pair<T, double>>();
			nodes.Add(null); //simplifies parent-child relations
		}

		void Swap(int first, int second)
		{
			Pair<T, double> temp = nodes[first];
			nodes[first] = nodes[second];
			nodes[second] = temp;
		}

		public void Push(T value, double priority)
		{
			int currentIndex = nodes.Count;
			int parentIndex = currentIndex / 2;
			nodes.Add(new Pair<T, double>(value, priority));

			while (nodes[parentIndex] != null)
			{
				if (nodes[currentIndex].second > nodes[parentIndex].second)//if current has lower priority (higher weight in our case) than parent
					break;

				Swap(currentIndex, parentIndex);
				currentIndex = parentIndex;
				parentIndex = currentIndex / 2;
			}
		}

		public T Pop()
		{
			if (nodes.Count < 2)
				throw new IndexOutOfRangeException();

			T result = nodes[1].first;
			nodes.RemoveAt(1); //remove root

			Rebuild();

			return result;
		}

		void Rebuild()
		{
			nodes.Insert(1, nodes[nodes.Count - 1]); //insert last leaf in place of root
			nodes.RemoveAt(nodes.Count - 1);

			int currentIndex = 1;
			int leftIndex = 2 * currentIndex;
			int rightIndex = 2 * currentIndex + 1;

			while (leftIndex < nodes.Count)
			{


				if (nodes[currentIndex].second > nodes[leftIndex].second && rightIndex >= nodes.Count)
				{
					Swap(currentIndex, leftIndex);
					currentIndex = leftIndex;
					leftIndex = 2 * currentIndex;
					rightIndex = 2 * currentIndex + 1;
				}
				else if (rightIndex < nodes.Count)
				{
					int childIndex = nodes[leftIndex].second <= nodes[rightIndex].second ? leftIndex : rightIndex;
					if (nodes[currentIndex].second > nodes[childIndex].second)
					{
						Swap(currentIndex, childIndex);
						currentIndex = childIndex;
						leftIndex = 2 * currentIndex;
						rightIndex = 2 * currentIndex + 1;
					}
					else
						break;
				}
				else
					break;
			}
		}
	}

}