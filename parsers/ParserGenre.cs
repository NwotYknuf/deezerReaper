using System.Collections.Generic;

namespace deezerAPI{

    public class ParserGenre : Parser{

        public ParserGenre(Parser next) : base(next) { }

        public override bool canParse(Dictionary<string, dynamic> data){
            return data["type"] == "genre";
        }

        public override Idded parse(Dictionary<string, dynamic> data){
            return new Genre((int)data["id"], (string)data["name"]);
        }

    }

}