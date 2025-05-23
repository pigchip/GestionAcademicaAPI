﻿using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    public class PropuestaRepository : IPropuestaRepository
    {
        private readonly AppDbContext _context;

        public PropuestaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Propuesta>> GetAllAsync()
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .ToListAsync();
        }

        public async Task<Propuesta?> GetByIdAsync(int id)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Propuesta>> FindAsync(Expression<Func<Propuesta, bool>> predicate)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Propuesta>> GetBySolicitudIdAsync(int idSolicitud)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .Where(p => p.IdSolicitud == idSolicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<Propuesta>> GetByEscuelaIdAsync(int idEscuela)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .Where(p => p.IdEscuela == idEscuela)
                .ToListAsync();
        }

        public async Task<IEnumerable<Propuesta>> GetByStatusAsync(string status)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Propuesta>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .Where(p => p.Fecha >= fechaInicio && p.Fecha <= fechaFin)
                .ToListAsync();
        }

        public async Task UpdateAsync(Propuesta propuesta)
        {
            var existingPropuesta = await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.Id == propuesta.Id);
            if (existingPropuesta == null)
                throw new KeyNotFoundException($"Propuesta con ID {propuesta.Id} no encontrada.");
            _context.Entry(existingPropuesta).CurrentValues.SetValues(propuesta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var propuesta = await _context.Propuestas.FindAsync(id);
            if (propuesta != null)
            {
                _context.Propuestas.Remove(propuesta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountBySolicitudAsync(int idSolicitud)
        {
            return await _context.Propuestas.CountAsync(p => p.IdSolicitud == idSolicitud);
        }

        public async Task<int> CountByEscuelaAsync(int idEscuela)
        {
            return await _context.Propuestas.CountAsync(p => p.IdEscuela == idEscuela);
        }

        public async Task<int> CountByStatusAsync(string status)
        {
            return await _context.Propuestas.CountAsync(p => p.Status == status);
        }

        public async Task<Propuesta?> GetLastPropuestaBySolicitudAsync(int idSolicitud)
        {
            return await _context.Propuestas
                .Include(p => p.Escuela)
                .Include(p => p.Materias)
                .Where(p => p.IdSolicitud == idSolicitud)
                .OrderByDescending(p => p.Fecha)
                .FirstOrDefaultAsync();
        }
    }
}