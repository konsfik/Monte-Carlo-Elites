using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Perfect_Mazes_Lib
{
    public static class PM_Maze_Serialization_Extensions
    {
        public static string Serialize_ToJson_String(
            this PM_Maze maze,
            Formatting formatting,
            TypeNameHandling typeNameHandling
            )
        {
            JsonSerializerSettings set = new JsonSerializerSettings();
            set.Formatting = formatting;
            set.TypeNameHandling = typeNameHandling;

            string ser = JsonConvert.SerializeObject(maze, set);
            return ser;
        }

        public static PM_Maze Deserialize_Maze_From_JSON_String(string serialized_maze)
        {
            PM_Maze deserialized_maze = JsonConvert.DeserializeObject<PM_Maze>(serialized_maze);
            return deserialized_maze;
        }
    }
}
