from quart import Quart, request, jsonify
import joblib
import surprise

app = Quart(__name__)

# Завантаження моделі
model = joblib.load('matrix_factorization_model.pkl')

@app.route('/predict', methods=['POST'])
async def predict():
    data = await request.get_json()
    user_id = data['UserId']
    potential_places_ids = data['PotentialPlacesIds']
    
    predictions = {}
    for place_id in potential_places_ids:
        prediction = model.predict(user_id, place_id)
        predictions[place_id] = prediction.est

    return jsonify(predictions)

if __name__ == '__main__':
    app.run(debug=True)
