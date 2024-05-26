import functools
import inspect
import logging

logging.basicConfig(level=logging.INFO)

def log_params(func):
    @functools.wraps(func)
    def wrapper(*args, **kwargs):
        sig = inspect.signature(func)
        params = {}
        bound_args = sig.bind(*args, **kwargs)
        for name, value in bound_args.arguments.items():
            params[name] = type(value).__name__
        logging.info(f"Function {func.__name__} called with parameters: {params}")
        return func(*args, **kwargs)
    return wrapper

@log_params
def merge_lists(*args):
    return sum(args, [])

# Wywołanie funkcji
print(merge_lists([1], [2, 3]))