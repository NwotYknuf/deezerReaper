using System;
using System.Collections.Generic;

namespace deezerAPI{

    public abstract class Parser{

        private Parser _next;

        public Parser(Parser next){
            _next = next;
        }

        public abstract bool canParse(Dictionary<string, dynamic> data);

        public abstract Idded parse(Dictionary<string, dynamic> data);

        public Idded tryParsing(Dictionary<string, dynamic> data){
            if(canParse(data)){
                return parse(data);
            }
            else{
                if(_next != null){
                    return _next.tryParsing(data);
                }
                else{
                    throw new ArgumentException("cannot parse given data");
                }
            }
        }

    }
}