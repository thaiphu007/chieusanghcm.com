
(function() {

    var $type = String;
    $type.__typeName = 'String';
    $type.__class = true;

    var $prototype = $type.prototype;
    /*Define Methods*/

    $prototype.endsWith = function(suffix) {
        /// <summary>Determines whether the end of this instance matches the specified string.</summary>
        /// <param name="suffix" type="String">A string to compare to.</param>
        /// <returns type="Boolean">true if suffix matches the end of this instance; otherwise, false.</returns>
        return (this.substr(this.length - suffix.length) === suffix);
    };

    $prototype.startsWith = function(prefix) {
        /// <summary >Determines whether the beginning of this instance matches the specified string.</summary>
        /// <param name="prefix" type="String">The String to compare.</param>
        /// <returns type="Boolean">true if prefix matches the beginning of this string; otherwise, false.</returns>
        return (this.substr(0, prefix.length) === prefix);
    };

    $prototype.trim = function() {
        /// <summary >Removes all leading and trailing white-space characters from the current String object.</summary>
        /// <returns type="String">The string that remains after all white-space characters are removed from the start and end of the current String object.</returns>
        return this.replace(/^\s+|\s+$/g, '');
    };

    $prototype.trimEnd = function() {
        /// <summary >Removes all trailing white spaces from the current String object.</summary>
        /// <returns type="String">The string that remains after all white-space characters are removed from the end of the current String object.</returns>
        return this.replace(/\s+$/, '');
    };

    $prototype.trimStart = function() {
        /// <summary >Removes all leading white spaces from the current String object.</summary>
        /// <returns type="String">The string that remains after all white-space characters are removed from the start of the current String object.</returns>
        return this.replace(/^\s+/, '');
    };

    $type.format = function() {
        /// <summary>Formart string with arguments "{n}"</summary>
        /// <returns type="Boolean">The string that replaced with arguments</returns>
        var s = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[i + 1]);
        }
        return s;
    };

    $prototype.ValidEmail = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(this);
    };
    $prototype.IsDigitOrDecimal = function(isDigit, isMoreThanZero) {
        /// <summary>Check string is integer or float valid.</summary>
        /// <param name="isDigit" type="String">true if check a string is integer; otherwise, false if check a string is float.</param>
        /// <param name="isMoreThanZero" type="String">true if check a integer or float greater than zero ; otherwise, false if check a integer or float equal zero.</param>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        isMoreThanZero = typeof isMoreThanZero !== 'undefined' ? isMoreThanZero : false;
        var result;
        if (isDigit)
            result = this.IsInteger;
        else
            result = this.IsFloat;
        if (result && isMoreThanZero) {
            if (this == '0')
                result = false;
        }
        return result;
    };


    $prototype.FormatCurrency = function(currency, indexCurrency) {
        /// <summary>Format string with currency.</summary>
        /// <param name="currency" type="String">char currency ex: $,VND,.....</param>
        /// <param name="indexCurrency" type="String">0: Last String, 1: First String</param>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var aDigits = parseFloat(this).toFixed(2).split(".");
        aDigits[0] = aDigits[0].split("").reverse().join("")
            .replace(/(\d{3})(?=\d)/g, "$1,").split("").reverse().join("");
        if (indexCurrency == 1)
            return $type.format("{0}{1}", aDigits.join("."), currency);
        else
            return $type.format("{0}{1}", currency, aDigits.join("."));
    };

    /// <summary>Check string is float valid.</summary>
    /// <returns type="Boolean">true if valid; otherwise, false.</returns>
    $prototype.IsFloat = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^((\d+(\.\d *)?)|((\d*\.)?\d+))$/);
        return pattern.test(this);
    };
    /// <summary>Check string is integer valid.</summary>
    /// <returns type="Boolean">true if valid; otherwise, false.</returns>
    $prototype.IsInteger = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^\d+$/);
        return pattern.test(this);
    };
    /// <summary>Check string is only Alphanumeric.</summary>
    /// <returns type="Boolean">true if valid; otherwise, false.</returns>
    $prototype.IsAlphanumeric = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^\w+$/i);
        return pattern.test(this);
    };
    /// <summary>Check string is bool valid.</summary>
    /// <returns type="Boolean">true if valid; otherwise, false.</returns>
    $prototype.IsBool = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^(true|1|false|0|yes|no)$/i);
        return pattern.test(this);
    };

    /// <summary>Check string is empty.</summary>
    /// <returns type="Boolean">true if valid; otherwise, false.</returns>
    $prototype.IsEmpty = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^\s*$/i);
        return pattern.test(this);
    };
    /// <summary>Check string is date valid (only format MM[-/.]dd[-/.]yyyy).</summary>
    /// <returns type="Boolean">true if valid; otherwise, false.</returns>
    $prototype.IsValidDate = function() {
        /// <summary>Check string email valid.</summary>
        /// <returns type="Boolean">true if valid; otherwise, false.</returns>
        var pattern = new RegExp(/^((((02)[- /.](0[1-9]|1[0-9]|2[0-8]))|((04|06|09|11)[- /.](0[1-9]|[12][0-9]|30))|((01|03|05|07|08|10|12)[- /.](0[1-9]|[12][0-9]|3[01])))[- /.]([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]|[0-9][1-9][0-9]{2}|[1-9][0-9]{3})|((02[- /.]29)[- /.](([0-9]{2}([02468][48]|[13579][26]|[2468]0))|(([02468][48]|[13579][26]|[2468]0)00))))$/i);
        return pattern.test(this);
    };


})(window);