1、尝试要点

1.1 MySQL的链接与使用方法，下载MySQL connector for .NET，然后通过类库发布一个DAO类，采用控制台程序验证对MySQL的读写正常
1.2 List泛型中找到最大值的写法 var totalSalary = employees.Sum(e => e.Salary);
1.3 下载图片的方法

2、下载一个Casuasel的插件，利用MVC完成展示网站

插件要求JQuery1.6.2，名称Circular Content Carousel

3、Exchange项目，实现内外网交互

内网的检索结果通过boneii@icloud.com发送给a-gon@163.com
外网接收邮件附件，内网将结果以附件形式发送
.net自带类库system.net.mail只能用smtpclient实现发送，而接收邮件需要第三方的库，例如s22.imap
参考：https://github.com/smiley22/S22.Imap

163的邮箱竟然提示我不安全？xm002 NO SELECT The login is not safe! Please update your mail client: http://mail.163.com/dashi

经过一次不安全认证后，就可以用我自己的收邮件客户端了