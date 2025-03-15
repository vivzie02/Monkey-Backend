import language_tool_python
from transformers import pipeline
from flask import Flask, jsonify, request


grammar_tool = language_tool_python.LanguageTool("de-AT")

logic_checker = pipeline("text-classification", model="oliverguhr/german-sentiment-bert")

app = Flask(__name__)

@app.route('/grammarCheck', methods = ['POST'])
def check_sentence():
    print("checking grammar")

    sentence = request.form.get('sentence')

    grammar_issues = grammar_tool.check(sentence)

    if(bool(grammar_issues)):
        return jsonify(False)

    logic_result = logic_checker(sentence)
    logic_label = logic_result[0]['label']

    logic_correct = logic_label != "negative"

    return jsonify(logic_correct)


if __name__ == "__main__":
    port = 8000
    app.run(host='0.0.0.0', port=port)