using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages;

public class IndexModel : PageModel
{
   public async Task OnGetAsync()
{
    // <snippet_search_linqQuery>
    IQueryable<string> genreQuery = from m in _context.Movie
                                    orderby m.Genre
                                    select m.Genre;
    // </snippet_search_linqQuery>

    var movies = from m in _context.Movie
                 select m;

    if (!string.IsNullOrEmpty(SearchString))
    {
        movies = movies.Where(s => s.Title.Contains(SearchString));
    }

    if (!string.IsNullOrEmpty(MovieGenre))
    {
        movies = movies.Where(x => x.Genre == MovieGenre);
    }

    // <snippet_search_selectList>
    Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
    // </snippet_search_selectList>
    Movie = await movies.ToListAsync();
}
}
