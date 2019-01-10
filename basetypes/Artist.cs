
namespace deezerAPI{
        
    public class Artist : Idded{

        private string _name;

        public string Name { get{ return _name; }}
        public Artist(int id, string name) : base(id){
            _name = name;
        }

    }
}