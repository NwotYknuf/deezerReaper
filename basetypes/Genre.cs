namespace deezerAPI{
    public class Genre : Idded{

        private string _name;

        public Genre(int id, string name) : base(id){ 
            _name = name;
        }

        public string Name { get{return _name; }}
    }
}