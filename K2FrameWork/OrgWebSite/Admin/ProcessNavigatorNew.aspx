<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessNavigatorNew.aspx.cs" Inherits="OrgWebSite.Admin.ProcessNavigatorNew" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>创建流程导航</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
 
/* Display styles */
.steps { clear: both; font-family: Arial; font-size: 12px; text-align: left; }
  .steps ul { display: table; table-layout: fixed; width: 100%; }
    .steps ul li { display: table-cell; list-style:none; vertical-align: middle;
                   _display: inline; *width: 120px; /* IE6/7 Hacks */ }

    /* CSS2.x browsers and IE fill width hacks - To fill 100% width they need to set the number the steps in the UL */
    .steps ul.1step li,  .steps ul.2steps li, .steps ul.3steps li, .steps ul.4steps li, .steps ul.5steps li,
    .steps ul.6steps li, .steps ul.7steps li, .steps ul.8steps li, .steps ul.9steps li, .steps ul.10steps li { display: inline; float:left; }
    .steps ul.1step  li  { width: 100%; }
    .steps ul.2steps li  { width: 50%;  }
    .steps ul.3steps li  { width: 33%;  }
    .steps ul.4steps li  { width: 25%;  }
    .steps ul.5steps li  { width: 20%;  }
    .steps ul.6steps li  { width: 16%;  }
    .steps ul.7steps li  { width: 14%;  }
    .steps ul.8steps li  { width: 12%;  }
    .steps ul.9steps li  { width: 11%;  }
    .steps ul.10steps li { width: 10%;  }

      .steps ul li.current {}
      .steps ul li.last a { }
      .steps ul li.completed {}
        .steps ul li.completed.last, .steps ul li.completedLast {}

      .steps ul li a { display:block; text-decoration:none; font-weight: normal; }
        .steps ul li.completed a:hover, .steps ul li.completed.last a:hover, .steps ul li.completedLast a:hover { cursor:pointer; }

        .steps ul li a em, .steps ul li a strong, .steps ul li a span, .steps ul li em, .steps ul li strong, .steps ul li span { display:block; font-style:normal; }
        .steps ul li a em, .steps ul li a strong, .steps ul li em, .steps ul li strong { font-weight:bold; }
        .steps ul li a span, .steps ul li a div, .steps ul li span, .steps ul li div  { height: 45px; /* For even heights */ }

/* Default colors */
.steps { text-shadow: 1px 1px #777; color:#CCCCCC; }
        .steps ul li a:link, .steps ul li a:visited, .steps ul li a:hover, .steps ul li a:active { color:#CCCCCC; }
        .steps ul li.completed.last a, .steps ul li.completed.last a:link, .steps ul li.completed.last a:visited, .steps ul li.completed.last a:hover, .steps ul li.completed.last a:active, .steps ul li.current a,
        .steps ul li.completedLast a, .steps ul li.completedLast a:link, .steps ul li.completedLast a:visited, .steps ul li.completedLast a:hover, .steps ul li.completedLast a:active,
        .steps ul li.completedLastEnd a, .steps ul li.completedLastEnd a:link, .steps ul li.completedLastEnd a:visited, .steps ul li.completedLastEnd a:hover, .steps ul li.completedLastEnd a:active,
        .steps ul li.current a, .steps ul li.current a:link, .steps ul li.current a:visited, .steps ul li.current a:hover,  .steps ul li.current a:active,
        .steps ul li.completed a, .steps ul li.completed a:link, .steps ul li.completed a:visited, .steps ul li.completed a:hover, .steps ul li.completed a:active
            { color:#FFFFFF; }
        .steps ul li.completed a:hover, .steps ul li.completed.last a:hover, .steps ul li.completedLast a:hover { color:#FFFF99; }

/* Color themes - No images */
      .steps ul li { padding: 0 5px 0 5px; background-color: #EBEBEB; }
        .steps ul li.current { background-color:#6699CC; }
        .steps ul li.last {  }
        .steps ul li.completed { background-color:#3366CC; }
        .steps ul li.completed.last, .steps ul li.completedLast { }

/* Color themes - Blue images theme. Sprites size: 23px x 71px */
     .steps ul.blue li { height:71px; padding: 0 20px 0 5px;
                          background: #EBEBEB url("data:image/gif;base64, R0lGODlhFwBjAcQfAP39/Yuv29fh7PLy8mWRxqDB5eXl5Xij0zdytVaJw8vc8Pr6+uDp8/j4+O7u7vX19Ud+vfb29rzN4Ojo6PH0+Wuc0erw9vj6/PX4+77R5/v7+3adxmaZzDNmzOvr6wAAACH5BAEAAB8ALAAAAAAXAGMBAAX/4CeOZGmeaKqubOu+cCzPdG3feK7vfO//wKBwSCwaj8ikcslsOp/QqHRKrVqv2OIAAPB4v+Bw2MHtis/hCdmMbnvWbPd5LW/D6+j1BC+elB18Y2WBgnGEd4RfeoleflyMXmsDkI4AgIyImI+QmYmLjJWQb4Ocm5qmnqiHpKd7oH+lqoF0sYazsnyhsa6Juqe2eGXAcmsPr1yTqcNyvquWkKzOvLe4ddHUyYll2djHy25rl4TN1NN8nYFl5niS0NXEXMa926figeTn73LXfOqn3Om4rKtTzB0AgOyQeYtlL+A3N8gG1hGwAIC8RBUKLGgAiUOAig8MJOKQscECRhw8/4KUiCYlhwIRFiB04/KAAossxbh8+cBioJ0VFGz8uTNAz5B4dpKE+ayOUg4HtjzI6eUpT5lOn9rcSNWqx6Mi23gteZKmV6M4xY6FibWlV6g3p7p9W+CBhpke3qbMyPWM3pRokYb5u7SBBr+Eo6YFQzglW4CN4fb9EvnrXcaRyfKq/NWigc+cM0boApqz4qkGODt+IDO1aps4VTu2OEB2UIsOZAd2XdmmBo68I9fVEDJ4YqmuODPN1hs55cYlNdiLHHga9AImGzauPvhv0N+I9db1rFMvbLnl6ca82N3raYlvRTcN/5T7XKVkG7ZXav++y4zgyWHVbgIqdUABARa40/9w7KlVk3NZucSUfve9x8d/2ElHVGfoJVXYAhSqReBPAHI00niCBXLgYoQ8BgmE2hyUQAc01mjjjTZKwIUAOPaIIwIBAIDBBj4WSeMBAFAgAAJG+ghBAA0AcECTPlaggZIQUPljABpoIIGWOCbAAAMWzAimjVwqeaaNT2Iw5Jo1HnDlknB2AKQGAHxZZwIRUGBBlnDemWedHSSQgQUUEEConEoyCeeTXOgJZwV9/lmnoJKuaaifZsIZQAQX8FhnmxgoWucBoNL5KJSD1klApYCuCWSUmZ5pqAAXdLomqqES+iQDXhJKqZ+EAjlarWDySeuln1pEqKEVIavlRweNmgH/AA1I2ySqzsJpKAALaFuksa1qmqqpZ5IrbpGvhqqrlpjWSWmosYIJwbXlnsmtBPVqeS+4RHp6rKO2ZhAqumDeGay3qb5LZbyTNvwovuv6uG+/VP4LQMC74ikBwckOQAEFHCfMZb7JnhtoBh67eqzDTSLAMgAByOsxxk1CsAXNp3oMsr87lwzmyR97K7IFCA/dcp0ONNAAzFSCW4BsKVnAxdSyISAAAAJQ3YGcAGDNWQcQCNClbDRaiYECqtEoaNs0JiDA2mPXyOUFYhPGpsEWVHYjt3nrdaPMeEYWZgR06z04lxgcoPiNhl7Q918Wjxb4U07OTHmVoDIguI93Nv6W/5GGauC5V002e3lKOfON+radW5XxybJT2e7pO8H7qQaO565l5JO7pO9obAtvbwYMhG08mPPiLmuzxXvbUwNTB9rsAxzsmUFFBRAaJLijJh92zxZlr+lNC1Sfbpqna7qA5Ms/fLLYZxJwJe7mP6zA2b5TaSUF+MufkSCwPw1gjkqMCqAAnYS+2hkpSIlTSpPEBAAFsm5c+xOdA3uUAA1cwIIX7BEC9geAz/XISh804Y0gACwDqtBGSFLeC2nEQnA9zkYso8DqJAi5BfipMYMDlgw3V6MEJGmHB3SbEP1WIwIckYl2amHdOiCBKyGxdhBYAP+YKMQryg4C8UBbF2VjRA4MeNErY5RNA3RINQ6EAAA7") no-repeat right -71px;
                          _background: #EBEBEB url("../images/steps/blue_step_sprite.gif") no-repeat right -71px; /* IE6 needs phisical images */ }
        .steps ul.blue li.current { background-color:#6699CC; }
        .steps ul.blue li.completed, .steps ul.blue li.completedLast, .steps ul.blue li.completedLastEnd { background-color:#3366CC; }

/* Color themes - Green images theme. Sprites size: 23px x 71px */
      .steps ul.green li { height:71px; padding: 0 20px 0 5px;
                           background: #EBEBEB url("data:image/gif;base64,R0lGODlhFwBjAcQfAJ3MktXk0eTk5Pb29vDw8K/0kJnrdnSxZWepVsPZvu3t7Yf2U8Hyrfj5+O7y7enp6Yu/fl2iS/Pz87z1oeHs3ujv5+/07vT2873atvf391SdQoP+R////zajAObm5gAAACH5BAEAAB8ALAAAAAAXAGMBAAX/4CeOZGmeaKqubOu+cCzPdG3feK7vfO//wKBwSCwaj8ikcslsOp/QqHRKrVqv2OKAwxkovuCwWLztjs/jshfN/qrb7DccLZ+TuWv73azf5/sKdYCBeINuhYaEfIOCgI19j3aRkoiMlZaLmH+Ql46depNzoW2jpJ+gp6KpqpmcZgKGarCaiYqbfVwciaVnvL2rccDBrZTExbesyMmxwnTGy7TMz6bT1MrWu81p2tvX2Mzew+HO47/Vzufo5X7Z6+zg2em/7mAPvt3x9GDcYwX8YRsY/PuyYcFABRsCDkxoUB7AhAIdEky4gQI/igolYsy4bmPDjhsjltuYkEAzkhzn/6CsKGzlhJYrRbZZWdDkNJop0eBk+WynzDM7c4oJ+tJYUKFgjk5Q0OkoUoROLWZyuqHoH6pCsU6wuQZrTq9LC3lNOXZDADxlBXope3bAg7EvBxCASnVC24lU1SatyzQPVr17lfZ9eBRw4J1bWzk1fJhm4ltHLSIL+nPoTsk6cVa2TNPkNc2raC7wPDMmMJekS5PcDJTkY5WrT4ZMDYckVzshuW28jZsia9WjL0L8x5C3nuESCRowfvz3nOUHmSYH06CBggMdsmvfzn07huoBuovvrgFAAwkQxqvPDqGBgwAR1o9HAEBCg/TyxUMY8B5BfvIADJBBAv91dwAFFFSAXf+B2wX4HoPb0WcBehBqt9978VUYQYANEFhhBwcQ4EAF/lVYnoAeVnhAAhU4sGCFF8L3IX0ZdPhhBxCISOKHG6J444oWKHgjAARcEN6MAEz4IoQ5GpkhhPTZlyKEIY5YIoTlSfljAgFcsCSDTR5ZIX0UDHhjjiPeuCEBNn4YopYmEtkAAVv6+CGHCsyYwHlTFpjjnG7uKUGf+ZXHJqH5hWjkl4XKiah8igbAqHw9tslkkQFcWSACCdT46Hp/JqDpf5yehx+Ejj5Z4IqLxmknlZhOul6ln6oXpqzqcerph6GOmp+uGZwKJoqq/neAAg44IOx/J1rKYKS4jqdBAgM4u+r/odGKFwG1DQDAK4q+yoeAAjV6C2O1CRT7K7nB3omuupAqEGS24nFYq3oPSCABveI1MIABZW1QQQMZADxWBAE0UEBZOFZrMFYdIBDAABOMxd4AFywMcQfNeqXdAV1q7FSDGD8cVIQJZEwVdzkWPPJ221b78naKiowTgCWfbGDKNq80XssmozQftzvZWmTPG6nX4wVBY7TeigNQQJN8RLos9HqcXlCBz/I1ifQGv3J4NaRFSp10owT8e3aiKW/ttJ9s2swgp2U+fOkFZoPN4JoKJ6QimxIAHCebFQcqoAFDEizBjBQQjPi5DRROZQAZBK53gSdaoDGVGLt9eaMOU/TsmgAW5P35rBMPsHbXpPdMauok/bef5rGLS7kEY6tnnsq1q3dgA6a/Le3EFyzA9XgHYPy13+MhLODNPysPfXcIUDCA6tOz7HjR1JeJO/fcUetA071/LAHtOmungfVWg5/dAQ1YQH7u6z8/M4juzT92BNZjPzMG/NHf1RAgAYp5rAPsE6DQEFCjinksgWM5QAbkVxYIjkUC4wtYCAAAOw==") no-repeat right -71px;
                           _background: #EBEBEB url("../images/steps/green_step_sprite.gif") no-repeat right -71px; /* IE6 needs phisical images */ }
        .steps ul.green li.current { background-color:#83FE47; }
        .steps ul.green li.completed, .steps ul.green li.completedLast, .steps ul.green li.completedLastEnd { background-color:#36A300; }

/* Color themes - Sprite positions & default colors - MUST BE AT THE END! */
      .steps ul li { background-position: right -71px; }
        .steps ul li.current { background-position: right -142px; }
        .steps ul li.end, .steps ul li.last, .steps ul li.completedLastEnd { background-position: right top; }
        .steps ul li.completed { background-position: right -213px; }
        .steps ul li.completed:last-child, .steps ul li.completedLast, .steps ul li.completed.last { background-position: right -284px; } /* class "last completed" only supported by modern browsers */
        .steps ul li:last-child { background-position: right top; } /* CSS3 browsers */
​

    </style>
    <script type="text/javascript" src="../Javascript/jquery-1.7.2.min.js"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 10px;">
        <div>
            <div class="nav">
                <p>
                    <%-- <uc1:Sitemap ID="Sitemap1" runat="server" />--%>
                    <uc1:Sitemap ID="Sitemap1" runat="server" />
                </p>
            </div>
        </div>
        <div class="steps">
            <ul class="blue 10steps">
                <li class="completed"><a href="#"><em>步骤 1</em><span>流程概要</span></a></li>
                <li class="completed"><a href="/Admin/RequestRoleManage.aspx"><em>步骤 2</em><span>配置入口节点</span></a></li>
                <li class="completed"><a href="/Admin/ProcessNodeManage.aspx"><em>步骤 3</em><span>配置审批节点</span></a></li>
                <li class="completed"><a href="/Admin/ApproveRuleManage.aspx"><em>步骤 4</em><span>配置流程规则</span></a></li>
                <li class="completed"><a href="/Admin/RuleManage.aspx"><em>步骤 5</em><span>规则表管理</span></a></li>
                <li class="completed"><a href="http://172.25.20.47:88/Workflow/K2FrameWork/FormTypeConfigureList.aspx" target="_blank"><em>步骤 6</em><span>配置表单类型</span></a></li>
                <li class="completed"><a href="http://172.25.20.47:88/Workflow/K2FrameWork/ApproveOpinionConfigureList.aspx" target="_blank"><em>步骤 7</em><span>配置审批意见</span></a></li>
                <li class="completed"><a href="http://172.25.20.47:88/Workflow/K2FrameWork/FormConfigureList.aspx" target="_blank"><em>步骤 8</em><span>配置表单</span></a></li>
                <li class="completed"><a href="#"><em>步骤 9</em><span>结束</span></a></li>
            </ul>
        </div>
     
      
    </div>
    
    </form>
</body>
</html>