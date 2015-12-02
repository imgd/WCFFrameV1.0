<%@ Page Language="C#" AutoEventWireup="true" CodePage="65001" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>My Tools</title>
    <style type="text/css">
        * {
            font-family: Consolas;
        }
    </style>
    <link rel="shortcut icon" href="h.ico" type="image/x-icon" />
    <script src="http://libs.baidu.com/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
    <script>

        $(function () {
            //ajax 请求
            $("#ajax_request").click(function () {
                var method = $("#request_method").val(),
                    url = $.trim($("#request_url").val()),
                    paras = $("#request_paras").val(),
                    token = $.trim($("#clientToken").val());

                if (IsNull(url)) {
                    alert("request_url 参数不能为空");
                    return;
                }

                //发送请求
                $.ajax({
                    url: "/handler/tool/WcfTestTool.ashx?f=ajax_post",
                    data: { para: paras, url: url, type: method, token: token },
                    cache: false,
                    dataType: "json",
                    success: function (result) {
                        SetMsg(result);
                    }
                });

            });
            //des 加/解密
            $("#des_convert").click(function () {
                var str = $("#txt_before").val(),
                    type = $("#sct_type").val();

                //发送请求
                $.ajax({
                    url: "/handler/tool/WcfTestTool.ashx?f=ajax_des_endes",
                    data: { str: str, type: type },
                    cache: false,
                    dataType: "json",
                    success: function (result) {
                        if (result.code == 200) {
                            $("#txt_after").val(result.data);
                            console.log("请求成功");
                        } else if (result.code == 500) {
                            console.log("请求异常");
                        }

                    }
                });
            });

            //get token
            $("#btn_gettoken").click(function () {
                var key = $.trim($("#txt_clientKey").val());
                if (key.length<=0) {
                    alert("请输入ClientKey")
                    return;
                }
                //发送请求
                $.ajax({
                    url: "/handler/tool/WcfTestTool.ashx?f=ajax_getclienttoken",
                    data: { clientkey: key },
                    cache: false,
                    dataType: "json",
                    success: function (result) {
                        if (result.code == 200) {
                            $("#txt_clientToken").val(result.data);
                            console.log("请求成功");
                        } else if (result.code == 500) {
                            console.log("请求异常");
                        }

                    }
                });
            });

            function IsNull(obj) {
                return obj.length <= 0;
            }
            function SetMsg(result) {
                if (result.code == 200) {
                    $("#response_msg").val(result.data);
                    $("#responsestatus").html(result.msg);
                    console.log("请求成功");
                } else if (result.code == 500) {
                    $("#responsestatus").html(result.data + " " + result.msg);
                    console.log("请求异常");
                }
            }
        });
        function ChangeTools() {

            var t_Tool_Name = $("#t_type_tool").val();
            if (t_Tool_Name == 0) {
                $("#t_checkwcf_post").show();
                $("#t_desconvert").hide();
                $("#t_clientToken").hide();
            } else if (t_Tool_Name == 1) {
                $("#t_checkwcf_post").hide();
                $("#t_desconvert").show();
                $("#t_clientToken").hide();
            } else {
                $("#t_checkwcf_post").hide();
                $("#t_desconvert").hide();
                $("#t_clientToken").show();
            }
        }

        var JSON = function () {
            var m = {
                '\b': '\\b',
                '\t': '\\t',
                '\n': '\\n',
                '\f': '\\f',
                '\r': '\\r',
                '"': '\\"',
                '\\': '\\\\'
            },
        s = {
            'boolean': function (x) {
                return String(x);
            },
            number: function (x) {
                return isFinite(x) ? String(x) : 'null';
            },
            string: function (x) {
                if (/["\\\x00-\x1f]/.test(x)) {
                    x = x.replace(/([\x00-\x1f\\"])/g, function (a, b) {
                        var c = m[b];
                        if (c) {
                            return c;
                        }
                        c = b.charCodeAt();
                        return '\\u00' +
                            Math.floor(c / 16).toString(16) +
                            (c % 16).toString(16);
                    });
                }
                return '"' + x + '"';
            },
            object: function (x) {
                if (x) {
                    var a = [], b, f, i, l, v;
                    if (x instanceof Array) {
                        a[0] = '[';
                        l = x.length;
                        for (i = 0; i < l; i += 1) {
                            v = x[i];
                            f = s[typeof v];
                            if (f) {
                                v = f(v);
                                if (typeof v == 'string') {
                                    if (b) {
                                        a[a.length] = ',';
                                    }
                                    a[a.length] = v;
                                    b = true;
                                }
                            }
                        }
                        a[a.length] = ']';
                    } else if (x instanceof Object) {
                        a[0] = '{';
                        for (i in x) {
                            v = x[i];
                            f = s[typeof v];
                            if (f) {
                                v = f(v);
                                if (typeof v == 'string') {
                                    if (b) {
                                        a[a.length] = ',';
                                    }
                                    a.push(s.string(i), ':', v);
                                    b = true;
                                }
                            }
                        }
                        a[a.length] = '}';
                    } else {
                        return;
                    }
                    return a.join('');
                }
                return 'null';
            }
        };
            return {
                copyright: '(c)2005 JSON.org',
                license: 'http://www.crockford.com/JSON/license.html',
                /*
                Stringify a JavaScript value, producing a JSON text.
                */
                stringify: function (v) {
                    var f = s[typeof v];
                    if (f) {
                        v = f(v);
                        if (typeof v == 'string') {
                            return v;
                        }
                    }
                    return null;
                },
                /*
                Parse a JSON text, producing a JavaScript value.
                It returns false if there is a syntax error.
                */
                parse: function (text) {
                    try {
                        return !(/[^,:{}\[\]0-9.\-+Eaeflnr-u \n\r\t]/.test(text.replace(/"(\\.|[^"\\])*"/g, ''))) && eval('(' + text + ')');

                    } catch (e) {
                        return false;
                    }
                }
            };
        }();


        var JsonUti = {
            //定义换行符
            n: "\n",
            //定义制表符
            t: "\t",
            //转换String
            convertToString: function (obj) {
                return JsonUti.__writeObj(obj, 1);
            },
            //写对象
            __writeObj: function (obj    //对象
                    , level             //层次（基数为1）
                    , isInArray) {       //此对象是否在一个集合内
                //如果为空，直接输出null
                if (obj == null) {
                    return "null";
                }
                //为普通类型，直接输出值
                if (obj.constructor == Number || obj.constructor == Date || obj.constructor == String || obj.constructor == Boolean) {
                    var v = obj.toString();
                    var tab = isInArray ? JsonUti.__repeatStr(JsonUti.t, level - 1) : "";
                    if (obj.constructor == String || obj.constructor == Date) {
                        //时间格式化只是单纯输出字符串，而不是Date对象
                        return tab + ("\"" + v + "\"");
                    }
                    else if (obj.constructor == Boolean) {
                        return tab + v.toLowerCase();
                    }
                    else {
                        return tab + (v);
                    }
                }

                //写Json对象，缓存字符串
                var currentObjStrings = [];
                //遍历属性
                for (var name in obj) {
                    var temp = [];
                    //格式化Tab
                    var paddingTab = JsonUti.__repeatStr(JsonUti.t, level);
                    temp.push(paddingTab);
                    //写出属性名
                    temp.push("\"" + name + "\"" + " : ");

                    var val = obj[name];
                    if (val == null) {
                        temp.push("null");
                    }
                    else {
                        var c = val.constructor;

                        if (c == Array) { //如果为集合，循环内部对象
                            temp.push(JsonUti.n + paddingTab + "[" + JsonUti.n);
                            var levelUp = level + 2;    //层级+2

                            var tempArrValue = [];      //集合元素相关字符串缓存片段
                            for (var i = 0; i < val.length; i++) {
                                //递归写对象                         
                                tempArrValue.push(JsonUti.__writeObj(val[i], levelUp, true));
                            }

                            temp.push(tempArrValue.join("," + JsonUti.n));
                            temp.push(JsonUti.n + paddingTab + "]");
                        }
                        else if (c == Function) {
                            temp.push("[Function]");
                        }
                        else {
                            //递归写对象
                            temp.push(JsonUti.__writeObj(val, level + 1));
                        }
                    }
                    //加入当前对象“属性”字符串
                    currentObjStrings.push(temp.join(""));
                }
                return (level > 1 && !isInArray ? JsonUti.n : "")                       //如果Json对象是内部，就要换行格式化
                    + JsonUti.__repeatStr(JsonUti.t, level - 1) + "{" + JsonUti.n     //加层次Tab格式化
                    + currentObjStrings.join("," + JsonUti.n)                       //串联所有属性值
                    + JsonUti.n + JsonUti.__repeatStr(JsonUti.t, level - 1) + "}";   //封闭对象
            },
            __isArray: function (obj) {
                if (obj) {
                    return obj.constructor == Array;
                }
                return false;
            },
            __repeatStr: function (str, times) {
                var newStr = [];
                if (times > 0) {
                    for (var i = 0; i < times; i++) {
                        newStr.push(str);
                    }
                }
                return newStr.join("");
            }
        };
        function ResultFormat() {
            var html = $("#response_msg").val();
            var htmlStr = JSON.parse(html);
            if (html.length) {
                $("#response_msg").val(htmlStr ? JsonUti.convertToString(htmlStr).toString() : html);
            }
        }
        function ParaFormat() {
            var _paras = $("#request_paras").val(),
              _parseStr = JSON.parse(_paras);

            if (_paras.length) {
                $("#request_paras").val(_parseStr ? JsonUti.convertToString(_parseStr).toString() : _paras);
            }
        }

    </script>
</head>
<body>
    <form id="form1">
        <%--ajax request--%>
        <div style="font-size: 18px; text-align: left; padding: 5px 20px 48px 20px; color: #413E37; border: 20px #413E37 solid">
            <b>Tools:<select id="t_type_tool" onchange="ChangeTools()">
                <option value="0">WCF Ajax Request tool</option>
                <option value="1">DES Keys Convert tool</option>
                <option value="2">GET Client Token tool</option>
            </select>
                <div id="t_checkwcf_post">
                    <h3>WCF服务测试(POST) beta v1.2</h3>
                    Request Method：
                        
                    <select id="request_method" style="border:1px dashed gray">
                        <option value="0">POST</option>
                        <option value="1">GET</option>
                    </select>
                    &nbsp;&nbsp;&nbsp; Client Token：
                     <input id="clientToken" placeholder="客户端身份验证token" style="width: 868px; border: 1px dashed #413E37;" type="text" />
                    <br />
                    <b>Request Url :</b> &nbsp;&nbsp;&nbsp;<input id="request_url" placeholder="服务请求地址，例：http://wcfservice.sucool.com/MobileProduct.svc/web/CreatGuid"
                        name="txt_url" type="text" style=" margin-top:5px;width: 1120px; border: 1px dashed #413E37; font-size: 16px; color: blue" /><br />
                    <br />
                    <b>Request Paras:&nbsp;&nbsp;</b>

                    <textarea onmouseout="ParaFormat()" id="request_paras" name="txt_paras" placeholder='Json格式参数，无参则不填，例：{"passWord":"a806f5026daa2","emailOrMobile":"yangshuai@doudoutao.com"}'
                        style="width: 1120px; height: 110px; border: 1px dashed #413E37; color: #DB1E00; font-size: 16px"
                        cols="20" rows="2"></textarea>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="ajax_request" type="button"
                    value="Request" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="btn_resuformat"
                        type="button" value="ResultFormat" onclick="ResultFormat()" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                
                <div id="responsestatus" style="padding-left: 190px; height: 16px; color: red;"></div>
                    <br />
                    <b>Response Msgs:&nbsp; &nbsp;</b><textarea id="response_msg" style="width: 1120px; border: 1px dashed #413E37; height: 275px; margin-top: 5px; color: #01760E; font-size: 14px"
                        cols="20" rows="2"></textarea>
                    <br />
                </div>

                <%--des ende--%>
                <div id="t_desconvert" style="display: none;">
                    <b>
                        <h3>DES KEY Convert 工具</h3>
                    </b>待加/解密字符串：<input id="txt_before" type="text" style="width: 300px; height: 25px; padding: 5px 5px 5px 10px; font-size: 16px; border: 1px dashed #413E37" /><br />
                    <br />
                    <select id="sct_type" style="width: 60px; height: 20px;">
                        <option value="0">加密</option>
                        <option value="1">解密</option>
                    </select>
                    <input id="des_convert" type="button" value="Convert" style="width: 150px; height: 23px;" />
                    <br />
                    <br />
                    加/解密后字符串：<input id="txt_after" type="text" style="width: 300px; height: 25px; padding: 5px 5px 5px 10px; font-size: 16px; border: 1px dashed #413E37" />
                </div>

                <div id="t_clientToken" style="display: none;">
                    <b>
                        <h3>GET Client Token 工具</h3>
                    </b>
                    <br />
                    ClientKey：&nbsp;&nbsp;<input id="txt_clientKey" type="text" style="width: 750px; height: 25px; padding: 5px 5px 5px 10px; font-size: 16px; border: 1px dashed #413E37" /><br />
                    <br />
                    <input id="btn_gettoken" type="button" value="GET TOKEN" style="width: 150px; height: 23px;" /><br />
                    <br />
                    ClientToken：<input id="txt_clientToken" type="text" style="width: 750px; height: 25px; padding: 5px 5px 5px 10px; font-size: 16px; border: 1px dashed #413E37" />
                    <br />
                </div>
        </div>
    </form>
</body>
</html>




