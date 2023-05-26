import argparse
import datetime
import os
import sys
import codecs

parser = argparse.ArgumentParser(description='Generate a MediatR command and Command handler for a specified name.')
parser.add_argument('name', type=str, help='The name of the command and command handler to generate.')
parser.add_argument('return_type', type=str, help='The return type of the command handler.')
parser.add_argument('fields', nargs='+', metavar='field:type', help='The fields and types of the command.')
args = parser.parse_args()

if not args.name.isidentifier():
    print('Invalid name. Please enter a valid identifier.')
    sys.exit()

command_name = args.name + 'Command'
handler_name = args.name + 'CommandHandler'
validator_name = args.name + 'Validator'
return_type = args.return_type

fields = {}
for field in args.fields:
    name, type_ = field.split(':')
    fields[name] = type_

field_declarations = '\n'.join([f'\t\tpublic {type_} {name} {{ get; set; }}' for name, type_ in fields.items()])
validator_rules = '\n'.join([f'\t\t\tRuleFor(x => x.{name}).NotEmpty();' for name in fields.keys()])

validator_template = f'''
public class {validator_name} : AbstractValidator<{command_name}>
{{
    public {validator_name}()
    {{
{validator_rules}
    }}
}}
'''

command_template = f'''using MediatR;
using FluentResults;
using FluentValidation;

namespace NamespacePlaceholder 
{{
    public class {command_name} : IRequest<Result<{return_type}>>
    {{
{field_declarations}
    }}

    public class {validator_name} : AbstractValidator<{command_name}>
        {{
            public {validator_name}()
            {{
{validator_rules}
            }}
        }}

}}


'''

handler_template = f'''using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace NamespacePlaceholder 
{{
    public class {handler_name} : IRequestHandler<{command_name}, Result<{return_type}>>
    {{
        public async Task<Result<{return_type}>> Handle({command_name} request, CancellationToken cancellationToken)
        {{
            throw new NotImplementedException();
        }}
    }}
}}

'''

date = datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')

if not os.path.exists(args.name):
    os.makedirs(args.name)

with codecs.open(f'{args.name}/{command_name}.cs', 'w', 'utf-8') as f:
    #f.write(f'// Generated by Python script on {date}\n\n')
    f.write(command_template)

with codecs.open(f'{args.name}/{handler_name}.cs', 'w', 'utf-8') as f:
    #f.write(f'// Generated by Python script on {date}\n\n')
    f.write(handler_template)

print(f'{command_name}.cs and {handler_name}.cs generated successfully!')