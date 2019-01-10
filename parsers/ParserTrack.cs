using System.Collections.Generic;

namespace deezerAPI{

    public class ParserTrack : Parser{

        public ParserTrack(Parser next) : base(next) { }

        public override bool canParse(Dictionary<string, dynamic> data){
            return data["type"] == "track";
        }

        public override Idded parse(Dictionary<string, dynamic> data){
            return new Track((int)data["id"], (string)data["title"]);
        }

    }

}