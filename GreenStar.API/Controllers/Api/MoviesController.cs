using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GreenStar.API.Models;
using AutoMapper;
using GreenStar.API.Dtos;
using GreenStar.API.Core;

namespace GreenStar.API.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        //GET api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            var movies = _unitOfWork.Movies.GetMoviesWithGenre();

            var movieDtos = movies.Select(Mapper.Map<Movie, MovieDto>);

            return movieDtos;
        }
       
        //GET api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _unitOfWork.Movies.Get(id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST api/movies
        //[Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            movie.DateAdded = DateTime.Now;

            _unitOfWork.Movies.Add(movie);
            _unitOfWork.Complete();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //PUT api/movies/id
        //[Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _unitOfWork.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _unitOfWork.Complete();

            return Ok();
        }

        // DELETE api/movies/id
        //[Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _unitOfWork.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _unitOfWork.Movies.Remove(movieInDb);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
