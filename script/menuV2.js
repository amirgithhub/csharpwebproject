function initmenu() { var e = document.getElementById("menuV2"), f = e.offsetHeight, t = e.getElementsByTagName("ul"), o = /msie|MSIE 6/.test(navigator.userAgent), i, r, u, n; if (o) for (var s = e.getElementsByTagName("li"), n = 0, h = s.length; n < h; n++) s[n].onmouseover = function () { this.className = "onhover" }, s[n].onmouseout = function () { this.className = "" }; for (n = 0; n < t.length; n++) i = t[n].parentNode, i.getElementsByTagName("a")[0].className += " arrow", t[n].style.right = i.offsetWidth + "px", t[n].style.top = i.offsetTop + "px", alignWithMainMenu && (r = getParentOffsetRoot(i.parentNode, 0), t[n].offsetTop + t[n].offsetHeight + r > f && (u = t[n].offsetHeight > f ? -r : f - t[n].offsetHeight - r, t[n].style.top = u + "px")), i.onmouseover = function () { o && (this.className = "onhover"); var n = this.getElementsByTagName("ul")[0]; n && (n.style.visibility = "visible", n.style.display = "block") }, i.onmouseout = function () { o && (this.className = ""), this.getElementsByTagName("ul")[0].style.visibility = "hidden", this.getElementsByTagName("ul")[0].style.display = "none" }; for (n = t.length - 1; n > -1; n--) t[n].style.display = "none" } function getParentOffsetRoot(n, t) { return n.id == "menuV2" ? t : (t += n.offsetTop, getParentOffsetRoot(n.parentNode.parentNode, t)) } var alignWithMainMenu = !1; window.addEventListener ? window.addEventListener("load", initmenu, !1) : window.attachEvent && window.attachEvent("onload", initmenu);