import functools
import logging

logging.basicConfig(level=logging.INFO)

def list_type(func):
    @functools.wraps(func)
    def wrapper(*args, **kwargs):
        for arg in args:
            if not isinstance(arg, list):
                raise TypeError(f"Argument {arg} is not a list.")
        return func(*args, **kwargs)
    return wrapper

@list_type
def merge_lists(*args):
    return sum(args, [])

print(merge_lists(1))