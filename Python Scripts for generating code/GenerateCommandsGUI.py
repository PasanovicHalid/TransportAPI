import argparse
import datetime
import os
import sys
import codecs
import tkinter as tk
from tkinter import filedialog, messagebox

def generate_code():
    base_path = base_path_entry.get()
    folder_path = folder_path_entry.get()
    command_type = type_var.get()
    command_name = name_entry.get()
    return_type = return_type_entry.get()
    fields = fields_entry.get().split('|')

    if not command_name.isidentifier():
        messagebox.showerror('Error', 'Invalid name. Please enter a valid identifier.')
        return

    project_name = base_path.split('/')[-1]
    folder_structure = folder_path.replace(base_path, '')

    namespace = project_name + folder_structure.replace('/', '.')
    request_type = command_type
    command_name = command_name + command_type
    handler_name = command_name + 'Handler'
    validator_name = command_name + 'Validator'

    if return_type == 'void':
        return_type = 'Result'
    else:
        return_type = f'Result<{return_type}>'

    fields_dict = {}
    for field in fields:
        name, type_ = field.split(':')
        fields_dict[name] = type_

    field_declarations = '\n'.join([f'\t\tpublic {type_} {name} {{ get; set; }}' for name, type_ in fields_dict.items()])
    validator_rules = '\n'.join([f'\t\t\tRuleFor(x => x.{name})\n\t\t\t\t.NotEmpty()\n\t\t\t\t.WithMessage("{name} is required");\n' for name in fields_dict.keys()])
    validator_rules = validator_rules[:-1]

    code_template = f'''using MediatR;
using FluentResults;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace {namespace}
{{
    public sealed class {command_name} : I{command_type}<{return_type}>
    {{
{field_declarations}
    }}

    internal sealed class {validator_name} : AbstractValidator<{command_name}>
    {{
        public {validator_name}()
        {{
{validator_rules}
        }}
    }}

    internal sealed class {handler_name} : IRequestHandler<{command_name}, {return_type}>
    {{
        public async Task<{return_type}> Handle({command_name} request, CancellationToken cancellationToken)
        {{
            throw new NotImplementedException();
        }}
    }}
}}

'''
    with codecs.open(f'{folder_path}/{command_name}.cs', 'w', 'utf-8') as f:
        f.write(code_template)

    messagebox.showinfo('Success', f'{command_name}.cs generated successfully!')

def browse_base_path():
    base_path = filedialog.askdirectory()
    base_path_entry.configure(state='normal')
    base_path_entry.delete(0, tk.END)
    base_path_entry.insert(tk.END, base_path)
    base_path_entry.configure(state='readonly')

def browse_folder_path():
    folder_path = filedialog.askdirectory()
    folder_path_entry.configure(state='normal')
    folder_path_entry.delete(0, tk.END)
    folder_path_entry.insert(tk.END, folder_path)
    folder_path_entry.configure(state='readonly')

root = tk.Tk()
root.title('Generate MediatR Requests GUI')
base_path_label = tk.Label(root, text='Base Path of the project:')
base_path_label.grid(row=0, column=0, sticky=tk.W)
base_path_entry = tk.Entry(root, width=150, state='readonly')
base_path_entry.grid(row=0, column=1, padx=5, pady=5)
base_path_button = tk.Button(root, text='Browse', command=browse_base_path)
base_path_button.grid(row=0, column=2, padx=5, pady=5)

folder_path_label = tk.Label(root, text='Folder Path (Where the request will get generated):')
folder_path_label.grid(row=1, column=0, sticky=tk.W)
folder_path_entry = tk.Entry(root, width=150, state='readonly')
folder_path_entry.grid(row=1, column=1, padx=5, pady=5)
folder_path_button = tk.Button(root, text='Browse', command=browse_folder_path)
folder_path_button.grid(row=1, column=2, padx=5, pady=5)

type_label = tk.Label(root, text='Type of request (Command or Query):')
type_label.grid(row=2, column=0, sticky=tk.W)
type_var = tk.StringVar(root)
type_var.set('Command')
type_dropdown = tk.OptionMenu(root, type_var, 'Command', 'Query')
type_dropdown.grid(row=2, column=1, padx=5, pady=5)

name_label = tk.Label(root, text='Name of the request:')
name_label.grid(row=3, column=0, sticky=tk.W)
name_entry = tk.Entry(root, width=150)
name_entry.grid(row=3, column=1, padx=5, pady=5)

return_type_label = tk.Label(root, text='Return Type (What the request returns. Type void if there is no return type):')
return_type_label.grid(row=4, column=0, sticky=tk.W)
return_type_entry = tk.Entry(root, width=150)
return_type_entry.grid(row=4, column=1, padx=5, pady=5)

fields_label = tk.Label(root, text='Fields (format is field_name:field_type). To have multiple formats use | between field inputs:')
fields_label.grid(row=5, column=0, sticky=tk.W)
fields_entry = tk.Entry(root, width=150)
fields_entry.grid(row=5, column=1, padx=5, pady=5)

generate_button = tk.Button(root, text='Generate', command=generate_code)
generate_button.grid(row=6, column=0, columnspan=3, padx=5, pady=5)

root.mainloop()