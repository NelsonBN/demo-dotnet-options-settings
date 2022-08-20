# .NET Options & Settings

|                  | Can use in singleton | No need to reload app | Change after resolving dependency | Has OnChange event | Lifecicle |
| :---             | :---                 | :---                  | :---                              | :---               | :---      |
| IOptions         | &check;              | &cross;               | &cross;                           | &cross;            | Singleton |
| IOptionsMonitor  | &check;              | &check;               | &check;                           | &check;            | Transient |
| IOptionsSnapshot | &cross;              | &check;               | &cross;                           | &cross;            | Scoped    |

## How can test?

To test, after call the endpoint in swagger, change the property `MY_NAME` in `appsettings.json`, while the process is stopped in the sleep thread and see the difference in the logs on console

```
appsettings.json
```
```json
{
  ...
  "MY_NAME": "Nelson"
}
```