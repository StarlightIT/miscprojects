using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadEvents
{
    public class Program
    {
        public class Metadata
        {
            public int NumberOfLandmarks { get; set; }
            public int NumberOfConnectingRoads { get; set; }
        }

        public class Road
        {
            public int RoadNumber { get; set; }
            public int LandmarkA { get; set; }
            public int LandmarkB { get; set; }
            public int CarryCapacity { get; set; }
        }

        public abstract class Event
        {
            public string EventType { get; set; }
        }

        public class BrEvent : Event
        {
            public int Road { get; set; }
            public int CarryCapacity { get; set; }
        }

        public class SupplyEvent : Event
        {
            public int LandmarkA { get; set; }
            public int LandmarkB { get; set; }
            public int Weight { get; set; }
        }

        public class LoadTester
        {
            public List<string> CalculateLoadSupport(List<string> input)
            {

                Metadata metadata = GetMetadata(input[0]);
                input.RemoveAt(0);
                List<Road> roads = GetRoads(metadata.NumberOfConnectingRoads, input);
                List<Event> roadEvents = GetEvents(metadata.NumberOfConnectingRoads, input);
                List<string> canSupportLoads = RunEvents(roads, roadEvents);
                return canSupportLoads;
            }

            private Metadata GetMetadata(string input)
            {
                var inputParts = input.Split(' ');
                Metadata metaData = new Metadata
                {
                    NumberOfLandmarks = int.Parse(inputParts[0]),
                    NumberOfConnectingRoads = int.Parse(inputParts[1]),
                };

                return metaData;
            }

            private List<Road> GetRoads(int numberOfConnectingRoads, List<string> input)
            {
                List<string> roadsRaw = input.Take(numberOfConnectingRoads).ToList();

                List<Road> roads = new List<Road>();
                for (int i = 0; i < roadsRaw.Count; i++)
                {
                    Road road = GetRoad(i, roadsRaw[i]);
                    roads.Add(road);
                }
                return roads;
            }

            private Road GetRoad(int i, string roadRaw)
            {
                string[] roadParts = roadRaw.Split(' ');
                Road road = new Road
                {
                    RoadNumber = i + 1,
                    LandmarkA = int.Parse(roadParts[0]),
                    LandmarkB = int.Parse(roadParts[1]),
                    CarryCapacity = int.Parse(roadParts[2])
                };
                return road;
            }

            private List<Event> GetEvents(int numberOfConnectingRoads, List<string> input)
            {
                List<Event> events = new List<Event>();
                List<string> eventsRaw = input.Skip(numberOfConnectingRoads + 1).ToList();

                foreach (var eventRaw in eventsRaw)
                {
                    Event ev = GetEvent(eventRaw);
                    events.Add(ev);
                }

                return events;
            }

            private Event GetEvent(string eventRaw)
            {
                string[] eventParts = eventRaw.Split(' ');

                switch (eventParts[0])
                {
                    case "B":
                    case "R":
                        return new BrEvent
                        {
                            EventType = eventParts[0],
                            Road = int.Parse(eventParts[1]),
                            CarryCapacity = int.Parse(eventParts[2])
                        };
                    case "S":
                        return new SupplyEvent
                        {
                            EventType = eventParts[0],
                            LandmarkA = int.Parse(eventParts[1]),
                            LandmarkB = int.Parse(eventParts[2]),
                            Weight = int.Parse(eventParts[3]),
                        };
                    default:
                        return null;
                }
            }

            private List<string> RunEvents(List<Road> roads, List<Event> events)
            {
                List<string> loadResults = new List<string>();

                foreach (var ev in events)
                {
                    if (ev.GetType() == typeof(BrEvent))
                    {
                        BrEvent brEvent = (BrEvent)ev;
                        Road road = roads.FirstOrDefault(r => r.RoadNumber == brEvent.Road);
                        road.CarryCapacity = brEvent.CarryCapacity;
                    }
                    if (ev.GetType() == typeof(SupplyEvent))
                    {
                        SupplyEvent sEvent = (SupplyEvent)ev;
                        List<Road> matchingRoads = roads.FindAll(r => r.LandmarkA == sEvent.LandmarkA && r.LandmarkB == sEvent.LandmarkB);
                        bool roadCanCarryLoads = false;

                        foreach(Road matchingRoad in matchingRoads)
                        {
                            bool canCarry = (sEvent.Weight <= matchingRoad.CarryCapacity);
                            if (canCarry)
                            {
                                roadCanCarryLoads = canCarry;
                                break;
                            }
                        }

                        string result = (roadCanCarryLoads) ? "1" : "0";
                        loadResults.Add(result);
                    }
                }

                return loadResults;
            }
        }

        public static void Main(string[] args)
        {
            string line;
            List<string> input = new List<string>();

            while ((line = Console.ReadLine()) != null)
            {
                input.Add(line);
            }
            LoadTester loadTester = new LoadTester();
            List<string> canSupportLoads = loadTester.CalculateLoadSupport(input);
            foreach(var canSupport in canSupportLoads)
            {
                Console.WriteLine(canSupport);
            }
        }
    }
}
