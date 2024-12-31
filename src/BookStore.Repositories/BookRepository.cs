using BookStore.Entities;

namespace BookStore.Repositories
{
    public class BookRepository
    {
        private readonly List<Book> booksList;
        public BookRepository()
        {
            booksList = new();
            booksList.Add(new Book() { Id = 1, Name = "En las profundidades", Author = "James Nestor", ISBN = "9786073920773", Editorial = "Planeta México", Synopsis = "El agua cubre el 70 por ciento de la superficie de la Tierra, pero tenemos escasa idea de lo que esconden sus profundidades. Este libro nos zambulle en un océano de historias, anécdotas y descubrimientos científicos que transformarán nuestra visión del mar y de nosotros mismos para siempre." });
            booksList.Add(new Book() { Id = 2, Name = "En Agosto nos vemos", Author = "Gabriel Garcia Marquez", ISBN = "9786073911290", Editorial = "Diana México", Synopsis = "Como cada 16 de agosto, Ana Magdalena Bach toma el transbordador para llegar a la isla donde está sepultada su madre, se registra en el hotel habitual, compra un ramo de gladiolos, pasa la tarde en el cementerio y, al día siguiente, regresa a casa con su familia. Sin embargo, esta vez el encuentro inesperado con un hombre cambiará para siempre su rutina invitándola cada año a escapar por una noche de la vida que ha construido con su esposo e hijos." });
            booksList.Add(new Book() { Id = 3, Name = "Alas de Sangre", Author = "Rebecca Yarros", ISBN = "9786073916240", Editorial = "Planeta", Synopsis = "Violet Sorrengail creía que se uniría al Cuadrante de los Escribas para vivir una vida tranquila, sin embargo, por órdenes de su madre, debe unirse a los miles de candidatos que, en el Colegio de Guerra de Basgiath, luchan por formar parte de la élite de Navarre: el Cuadrante de los Jinetes de dragón. Cuando eres más pequeña y frágil que los demás tu vida corre peligro, porque los dragones no se vinculan con humanos débiles." });
        }
        public List<Book> Get() 
        { 
            return booksList;
        }
        public Book? Get(int id)
        {
            return booksList.FirstOrDefault(x => x.Id == id);
        }
        public void Add(Book book)
        {
            var lastItem = booksList.MaxBy(x => x.Id);
            book.Id = lastItem is null ? 1 : lastItem.Id + 1; 
            booksList.Add(book);
        }
        public void Update(int id, Book book)
        {
            var item = Get(id);
            if(item is not null)
            {
                item.Name = book.Name;
                item.Author = book.Author;
                item.ISBN = book.ISBN;
                item.Editorial = book.Editorial;
                item.Synopsis = book.Synopsis;
            }
        }
        public void Delete(int id)
        {
            var item = Get(id);
            if(item is not null)
                booksList.Remove(item);
        }
    }
}
