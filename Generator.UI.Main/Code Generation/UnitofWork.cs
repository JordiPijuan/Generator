using System;
using (ENTER THE FULL NAMESPACE TO YOUR MODELS);

public class UnitOfWork : IUnitOfWork
{
    private DefaultModel _context;

    public UnitOfWork(DefaultModel context)
    {
        _context = context;
    }

	//Delete this default constructor if using an IoC container
	public UnitOfWork()
	{
		_context = new DefaultModel();
	}
	
    
    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
