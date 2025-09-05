using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Renova.Service.Helpers
{
    public static class FiltroHelper
    {
        public static IQueryable<T> ApplyDecimalFilter<T>(IQueryable<T> query, string? filtro, Expression<Func<T, decimal?>> selector)
        {
            if (string.IsNullOrWhiteSpace(filtro))
                return query;

            if (filtro.Contains("|")) // intervalo
            {
                var parts = filtro.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (decimal.TryParse(parts[0], out var min) && decimal.TryParse(parts[1], out var max))
                    query = query.Where(p => EF.Property<decimal?>(p, ((MemberExpression)selector.Body).Member.Name) >= min
                                          && EF.Property<decimal?>(p, ((MemberExpression)selector.Body).Member.Name) <= max);
            }
            else if (filtro.StartsWith("<"))
            {
                if (decimal.TryParse(filtro[1..], out var value))
                    query = query.Where(p => EF.Property<decimal?>(p, ((MemberExpression)selector.Body).Member.Name) < value);
            }
            else if (filtro.StartsWith(">"))
            {
                if (decimal.TryParse(filtro[1..], out var value))
                    query = query.Where(p => EF.Property<decimal?>(p, ((MemberExpression)selector.Body).Member.Name) > value);
            }
            else if (filtro.StartsWith("="))
            {
                if (decimal.TryParse(filtro[1..], out var value))
                    query = query.Where(p => EF.Property<decimal?>(p, ((MemberExpression)selector.Body).Member.Name) == value);
            }
            else if (decimal.TryParse(filtro, out var equalValue)) // caso sem operador, assume igualdade
            {
                query = query.Where(p => EF.Property<decimal?>(p, ((MemberExpression)selector.Body).Member.Name) == equalValue);
            }

            return query;
        }

        public static IQueryable<T> ApplyDateFilter<T>(IQueryable<T> query, string? filtro, Expression<Func<T, DateTime?>> selector)
        {
            if (string.IsNullOrWhiteSpace(filtro))
                return query;

            // Obter o nome da propriedade
            var propName = ((MemberExpression)selector.Body).Member.Name;

            if (filtro.Contains("|")) // intervalo
            {
                var parts = filtro.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (DateTime.TryParse(parts[0], out var min) && DateTime.TryParse(parts[1], out var max))
                    query = query.Where(p => EF.Functions.DateDiffDay(EF.Property<DateTime>(p, propName), min) <= 0
                                          && EF.Functions.DateDiffDay(EF.Property<DateTime>(p, propName), max) >= 0);
            }
            else if (filtro.StartsWith("<"))
            {
                if (DateTime.TryParse(filtro[1..], out var value))
                    query = query.Where(p => EF.Functions.DateDiffDay(EF.Property<DateTime>(p, propName), value) > 0);
            }
            else if (filtro.StartsWith(">"))
            {
                if (DateTime.TryParse(filtro[1..], out var value))
                    query = query.Where(p => EF.Functions.DateDiffDay(EF.Property<DateTime>(p, propName), value) < 0);
            }
            else if (filtro.StartsWith("="))
            {
                if (DateTime.TryParse(filtro[1..], out var value))
                    query = query.Where(p => EF.Functions.DateDiffDay(EF.Property<DateTime>(p, propName), value) == 0);
            }
            else if (DateTime.TryParse(filtro, out var equalValue))
            {
                query = query.Where(p => EF.Functions.DateDiffDay(EF.Property<DateTime>(p, propName), equalValue) == 0);
            }

            return query;
        }
    }
}
