using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Data;
using Pustok.Models;

namespace Pustok.Areas.Manage.Controllers;

[Area("Manage")]
public class TagController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var tags = await context.Tags.ToListAsync();
        return View(tags);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tag tag)
    {
        if (!ModelState.IsValid)
        {
            return View(tag);
        }

        if (context.Tags.Any(t => t.Name == tag.Name))
        {
            ModelState.AddModelError("Name", "This tag name already exists");
            return View(tag);
        }

        context.Tags.Add(tag);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var tag = await context.Tags.FindAsync(id);
        if (tag == null) return NotFound();
        return View(tag);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Tag tag)
    {
        if (id != tag.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            return View(tag);
        }

        if (context.Tags.Any(t => t.Name == tag.Name && t.Id != id))
        {
            ModelState.AddModelError("Name", "This tag name already exists");
            return View(tag);
        }

        try
        {
            context.Update(tag);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TagExists(tag.Id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var tag = await context.Tags
            .Include(t => t.BookTags)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null) return NotFound();

        if (tag.BookTags.Any())
        {
            TempData["Error"] = "This tag is being used by books and cannot be deleted.";
            return RedirectToAction(nameof(Index));
        }

        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TagExists(int id)
    {
        return context.Tags.Any(e => e.Id == id);
    }
}
