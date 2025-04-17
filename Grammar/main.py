import language_tool_python
from flask import Flask, jsonify, request
import spacy

# Initialize LanguageTool for Austrian German
grammar_tool = language_tool_python.LanguageTool("de-AT")

# Initialize spaCy for German parsing
nlp = spacy.load("de_core_news_sm")

app = Flask(__name__)

def manual_grammar_checks(sentence):
    is_ok = True

    # Check syntax structure with spaCy
    doc = nlp(sentence)

    # Check if sentence contains at least one verb
    has_verb = any(token.pos_ in ["VERB", "AUX"] for token in doc)
    if not has_verb:
        is_ok = False

    return is_ok


@app.route('/grammarCheck', methods=['POST'])
def check_sentence():
    sentence = request.form.get('sentence')
    if not sentence:
        return jsonify({"error": "No sentence provided"}), 400

    is_ok = True
    # Check grammar with LanguageTool
    grammar_issues = grammar_tool.check(sentence)
    if grammar_issues:
        is_ok = False

    is_ok = manual_grammar_checks(sentence)
    
    # Return combined result
    return jsonify({
        "is_ok": is_ok
    })


if __name__ == "__main__":
    port = 8000
    app.run(host='0.0.0.0', port=port)
