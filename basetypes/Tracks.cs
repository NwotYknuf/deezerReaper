
namespace deezerAPI{
    public class Track : Idded{

        private string _title;

        public Track(int id, string title):base(id){ 
            _title = title;
        }

        public string Title { get{ return _title; }}
        
    }
}